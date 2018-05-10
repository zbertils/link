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
            // set the frame header to the default PCM for the main engine codes
            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default);

            return GetDtc(cable, this, CodeType);
        }

        private List<DiagnosticTroubleCode> GetDtc(
            OBD2.Cables.Cable cable,
            DtcParameterIdentification specialMode,
            DiagnosticTroubleCode.CodeType codeType)
        {
            List<DiagnosticTroubleCode> codes = new List<DiagnosticTroubleCode>();

            try
            {
                string dtcstring = cable.Communicate(specialMode);

                // possible there is no data so check
                if (!string.IsNullOrEmpty(dtcstring) &&
                    !dtcstring.Contains(OBD2.Protocols.Elm327.Responses.Error))
                {
                    string[] dataLines = ParameterIdentification.PrepareResponseString(dtcstring);
                    foreach (string line in dataLines)
                    {
                        byte[] dtcBytes = ParameterIdentification.ParseStringValues(line);

                        // make sure the correct pid was received
                        if (dtcBytes[ParameterIdentification.ResponseByteOffsets.Mode] - 0x40 == specialMode.Mode)
                        {
                            byte[] dtcNumbers = dtcBytes.Skip(1).ToArray(); // skip the first byte which is the mode

                            // check if the response is empty, some protocols will return data with all zeros
                            if (!dtcNumbers.All(value => value == 0))
                            {
                                // the CAN protocols use a byte for how many codes are received, ISO based protocols do not,
                                // TODO: determine if this is actually the case or not, they may all be the same
                                if (cable.Protocol == Protocols.Protocol.HighSpeedCAN ||
                                    cable.Protocol == Protocols.Protocol.LowSpeedCAN)
                                {
                                    dtcNumbers = dtcNumbers.Skip(1).ToArray();
                                }

                                // each code should be two bytes, meaning we need an even number of bytes to parse correctly
                                if (dtcNumbers.Length % 2 == 0)
                                {
                                    for (int index = 0; index < dtcNumbers.Length; index += 2)
                                    {
                                        // it is possible to have zero values padded out in the packet, check if this is the case
                                        if (dtcNumbers[index] != 0 ||
                                            dtcNumbers[index + 1] != 0)
                                        {
                                            string firstByte = dtcNumbers[index].ToString(Protocols.ToHexFormat);
                                            string secondByte = dtcNumbers[index + 1].ToString(Protocols.ToHexFormat);

                                            // the code is still in elm327 encoded format, e.g. "4670" which would be DTC B0670
                                            string elm327code = firstByte + secondByte;
                                            DiagnosticTroubleCode code = new DiagnosticTroubleCode(elm327code, codeType);

                                            // see if the code exists in the list of known codes to get the description
                                            Utilities.IniFileEntry entry = link.Globals.dtcs.Find(e => e.Key == code.Code);
                                            if (entry != null)
                                            {
                                                code.Description = entry.Value;
                                            }

                                            // finally add the code to the list after the description was checked
                                            codes.Add(code);
                                        }
                                        else
                                        {
                                            Diagnostics.DiagnosticLogger.Log("Skipping zero valued DTC");
                                        }
                                    }
                                }
                                else
                                {
                                    Diagnostics.DiagnosticLogger.Log("Number of bytes to work with for DTC check is not an expected even number, got " + dtcNumbers.Length + " total bytes");
                                }
                            }
                            else
                            {
                                Diagnostics.DiagnosticLogger.Log("Received correct mode and valid DTC data, but it is all zeros, no DTC to report");
                            }
                        }
                        else
                        {
                            Diagnostics.DiagnosticLogger.Log("Mode returned for DTC check is invalid, received " + (dtcBytes[ParameterIdentification.ResponseByteOffsets.Mode] - 0x40) + ", and expected " + specialMode.Mode);
                        }
                    }
                }
                else
                {
                    // maybe do something, most likely there are no codes and "NO DATA" was returned
                    Diagnostics.DiagnosticLogger.Log("It doesn't look like there are any trouble codes to be had...");
                }
            }
            catch (Exception ex)
            {
                Diagnostics.DiagnosticLogger.Log("Could not get trouble codes due to exception", ex);
            }

            return codes;
        }

    }
}
