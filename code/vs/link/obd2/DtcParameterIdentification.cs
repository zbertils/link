using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Extensions;
using OBD2.Cables;

namespace OBD2
{
    public class DtcParameterIdentification : ParameterIdentification
    {
        public readonly DiagnosticTroubleCode.CodeType CodeType;

        public DtcParameterIdentification(
            DiagnosticTroubleCode.CodeType codeType,
            string name,
            byte mode,
            short pid,
            byte dataByteCount,
            string formulaString,
            string units,
            string group,
            string pidType,
            string description = "",
            string header = "") :
            base(name, mode, pid, dataByteCount, formulaString, units, group, pidType, description, header)
        {
            this.CodeType = codeType;
        }

        /// <summary>
        /// Converts the special mode into a string suitable for sending to ELM327 cables.
        /// </summary>
        /// <returns> The string representing the mode only in ELM327 format. </returns>
        public override string Pack()
        {
            return Mode.ToString("X2") + OBD2.Protocols.Elm327.EndOfLine;
        }

        public List<DiagnosticTroubleCode> RequestTroubleCodes(OBD2.Cables.Cable cable)
        {
            // TODO: set frame headers for other protocols

            // set the frame header to the default PCM for the main engine codes
            if (cable.Protocol == Protocols.Protocol.HighSpeedCAN11 ||
                cable.Protocol == Protocols.Protocol.LowSpeedCAN11)
            {
                Protocols.Elm327.SetFrameHeader(Protocols.CAN.Headers.Default);
            }
            else if (cable.Protocol == Protocols.Protocol.VPW)
            {
                Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default);
            }

            return GetDtc(cable, this, CodeType);
        }

        private List<DiagnosticTroubleCode> GetDtc(
            Cable cable,
            DtcParameterIdentification specialMode,
            DiagnosticTroubleCode.CodeType codeType)
        {
            List<DiagnosticTroubleCode> codes = new List<DiagnosticTroubleCode>();

            try
            {
                String dtcString = cable.Communicate(specialMode);
                Diagnostics.DiagnosticLogger.Log("GetDtc: mode " + this.Mode + " received \"" + dtcString + "\"");

                // possible there is no data so check
                if (!string.IsNullOrEmpty(dtcString) && !dtcString.Contains(Protocols.Elm327.Responses.NoData))
                {
                    String[] dataLines = ParameterIdentification.PrepareResponseString(dtcString);
                    if (dataLines != null && dataLines.Length > 0)
                    {
                        dataLines = prepMarkedLines(dataLines);
                        foreach (String line in dataLines)
                        {
                            byte[] dtcBytes = ParameterIdentification.ParseStringValues(line);
                            if (dtcBytes != null && dtcBytes.Length > 0)
                            {

                                // make sure the correct pid was received
                                if (dtcBytes[ParameterIdentification.ResponseByteOffsets.Mode] - 0x40 == specialMode.Mode)
                                {
                                    byte[] dtcNumbers = dtcBytes.Skip(1).ToArray();

                                    // check if the returned numbers are all zeros, some times the data is not valid
                                    bool allZeros = true;
                                    foreach (int b in dtcNumbers)
                                    {
                                        if (b != 0 && b != 0xAA)
                                        { // 0 for non-CAN vehicles, and 0xAA for CAN
                                            allZeros = false;
                                            break;
                                        }
                                    }
                                    // check if the response is empty, some protocols will return data with all zeros
                                    if (!allZeros)
                                    {
                                        // the CAN protocols use a byte for how many codes are received, ISO based protocols do not,
                                        if (cable.Protocol == Protocols.Protocol.HighSpeedCAN11 ||
                                                cable.Protocol == Protocols.Protocol.LowSpeedCAN11 ||
                                                cable.Protocol == Protocols.Protocol.HighSpeedCAN29 ||
                                                cable.Protocol == Protocols.Protocol.LowSpeedCAN29)
                                        {
                                            dtcNumbers = dtcNumbers.Skip(1).ToArray();
                                        }

                                        // each code should be two bytes, meaning we need an even number of bytes to parse correctly
                                        //                                    if (dtcNumbers.length % 2 == 0) {
                                        {
                                            for (int index = 0; index < dtcNumbers.Length - 1; index += 2)
                                            {
                                                // it is possible to have zero values padded out in the packet, check if this is the case
                                                if (!(dtcNumbers[index] == 0 && dtcNumbers[index + 1] == 0) &&
                                                        !(dtcNumbers[index] == 0xAA && dtcNumbers[index + 1] == 0xAA))
                                                {
                                                    string firstByte = dtcNumbers[index].ToString(Protocols.ToHexFormat);
                                                    string secondByte = dtcNumbers[index + 1].ToString(Protocols.ToHexFormat);

                                                    // the code is still in elm327 encoded format, e.g. "4670" which would be DTC B0670
                                                    String elm327code = firstByte + secondByte;
                                                    DiagnosticTroubleCode code = new DiagnosticTroubleCode(elm327code, codeType);

                                                    // see if the code exists in the list of known codes to get the description
                                                    Utilities.IniFileEntry entry = link.Globals.dtcs.Find(e => e.Key == code.Code);
                                                    if (entry != null)
                                                    {
                                                        code.Description = entry.Value;
                                                    }

                                                    // finally add the code to the list after the description was checked
                                                    Diagnostics.DiagnosticLogger.Log("GetDtc: finished decoding dtc " + code.Code);
                                                    codes.Add(code);
                                                }
                                                else
                                                {
                                                    Diagnostics.DiagnosticLogger.Log("GetDtc: skipping zero values DTC");
                                                }
                                            }
                                        }
                                        if (dtcNumbers.Length % 2 != 0)
                                        {
                                            Diagnostics.DiagnosticLogger.Log("GetDtc: number of bytes to work with for dtc check is not an expected even number, got" + dtcNumbers.Length + " total bytes, ignoring last byte");
                                        }
                                    }
                                    else
                                    {
                                        Diagnostics.DiagnosticLogger.Log("GetDtc: Received correct mode and valid DTC data, but it is all zeros, no DTC to report");
                                    }
                                }
                                else
                                {
                                    Diagnostics.DiagnosticLogger.Log("GetDtc: mode returned for DTC check is invalid, received " + (dtcBytes[ParameterIdentification.ResponseByteOffsets.Mode] - 0x40) + ", and expected " + specialMode.Mode);
                                }
                            }
                            else
                            {
                                Diagnostics.DiagnosticLogger.Log("GetDtc: dtcBytes is null or zero sized");
                            }
                        } // end for (String line : dataLines)
                    }
                    else
                    {
                        Diagnostics.DiagnosticLogger.Log("dataLines was null, nothing returned for mode " + this.Mode);
                    }
                }
                else
                {
                    // maybe do something, most likely there are no codes and "NO DATA" was returned
                    Diagnostics.DiagnosticLogger.Log("GetDtc: it doesn't look like there are any trouble codes to be had");
                }
            }
            catch (Exception ex)
            {
                Diagnostics.DiagnosticLogger.Log("GetDtc: could not get trouble codes due to exception:\n", ex);
            }

            return codes;
        }

    }
}
