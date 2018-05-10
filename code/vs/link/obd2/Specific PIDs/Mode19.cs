using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Extensions;
using OBD2.Cables;

namespace OBD2.SpecificPids
{
    public class Mode19 : ParameterIdentification
    {

        public static class RequestType
        {
            public const byte ImmatureCode = 0x01;
            public const byte CurrentCode = 0x02;
            public const byte Reserved1 = 0x04;
            public const byte Reserved2 = 0x08;
            public const byte FreezeFrameAvailable = 0x10;
            public const byte OldCode = 0x20;
            public const byte PendingCode = 0x40;
            public const byte MILActive = 0x80;

            public const byte ActiveAndPending = (byte)(RequestType.CurrentCode + RequestType.PendingCode + RequestType.FreezeFrameAvailable + RequestType.MILActive);
            public const byte ActiveOnly = (byte)(RequestType.CurrentCode + RequestType.FreezeFrameAvailable + RequestType.MILActive);
            public const byte PendingOnly = (byte)(RequestType.PendingCode + RequestType.FreezeFrameAvailable + RequestType.MILActive);

            public const byte All = 0xFF;
        }

        public static readonly string[] StatusByteEncodedBitDescription =
        {
            "Insufficient data to consider malfunction",
            "Current code present at time of request",
            "Manufacturer specific status",
            "Manufacturer specific status",
            "Stored trouble code",
            "Warning lamp previously illuminated for this code, malfunction not currently detected, code not yet erased",
            "Warning lamp pending for this code, not illuminated but malfunction was detected",
            "Warning lamp illuminated for this code"
        };

        /// <summary>
        /// Instantiates a new instance of the Mode19 class. This class should not be used on its own.
        /// </summary>
        public Mode19() :
            base("Diagnostic Trouble Code Status", 0x19, RequestType.All, 0x01, "", "", "DTC", "Diagnostic")
        {
            Header = Protocols.J1850.Headers.PCM;
        }

        public override byte PacketSize { get { return 0x01; } }

        public List<Tuple<DiagnosticTroubleCode, string>> RequestAllDtcStatuses(OBD2.Cables.Cable cable)
        {
            List<Tuple<DiagnosticTroubleCode, string>>  statuses = new List<Tuple<DiagnosticTroubleCode, string>>();

            string response = cable.Communicate(this);
            string[] responses = ParameterIdentification.PrepareResponseString(response);
            if (responses != null)
            {
                foreach (string individualResponse in responses)
                {
                    byte[] responseBytes = ParameterIdentification.ParseStringValues(individualResponse);
                    if (responseBytes != null)
                    {
                        if (responseBytes.Length == 4)
                        {
                            if (responseBytes[0] - 0x40 == this.Mode)
                            {
                                string firstByte = responseBytes[1].ToString(Protocols.ToHexFormat);
                                string secondByte = responseBytes[2].ToString(Protocols.ToHexFormat);

                                // the code is still in elm327 encoded format, e.g. "4670" which would be DTC B0670
                                string elm327code = firstByte + secondByte;
                                DiagnosticTroubleCode code = new DiagnosticTroubleCode(elm327code, DiagnosticTroubleCode.CodeType.StatusCheck);

                                string codeStatusDescription = GetStatusDescription(responseBytes[3]);

                                statuses.Add(new Tuple<DiagnosticTroubleCode, string>(code, codeStatusDescription));
                            }
                            else
                            {
                                Diagnostics.DiagnosticLogger.Log("Invalid mode for mode 19 response line \"" + individualResponse + "\"");
                            }
                        }
                        else
                        {
                            Diagnostics.DiagnosticLogger.Log("Received a mode 19 response line that did not have 4 bytes. Received \"" + individualResponse + "\"");
                        }
                    }
                    else
                    {
                        Diagnostics.DiagnosticLogger.Log("ParseStringValues() returned null for response \"" + individualResponse ?? string.Empty + "\"");
                    }
                }
            }
            else
            {
                Diagnostics.DiagnosticLogger.Log("PrepareResponseString() returned null for \"" + response ?? string.Empty + "\"");
            }

            return statuses;
        }

        /// <summary>
        /// Gets the most severe description of the status bits.
        /// </summary>
        /// <param name="statusValue"> The 8 status bit value to obtain the description for. </param>
        /// <returns> The most severe description of the status bits. </returns>
        private string GetStatusDescription(int statusValue)
        {
            string description = string.Empty;

            for (int index = 0; statusValue > 0 && index < 8; index++)
            {
                int bitValue = statusValue & 0x1;
                if (bitValue > 0)
                {
                    // since the index counts up, and the severity increases with each bit only save the most severe bit
                    description = StatusByteEncodedBitDescription[index];
                }

                // decrease the value by shifting the bits over by one
                statusValue = statusValue >> 1;
            }

            return description;
        }

        public override string Pack()
        {
            return Mode.ToString("X2") + PID.ToString("X2") + "FF00" + OBD2.Protocols.Elm327.EndOfLine;
        }

        public override string SimulatedResponse()
        {
            return
                "59 A9 57 01" + Protocols.Elm327.EndOfLine +
                "59 A9 58 01" + Protocols.Elm327.EndOfLine +
                "59 B8 02 01" + Protocols.Elm327.EndOfLine +
                "59 D0 16 11" + Protocols.Elm327.EndOfLine +
                "59 A9 57 3F" + Protocols.Elm327.EndOfLine +
                "59 A9 57 25" + Protocols.Elm327.EndOfLine +
                "59 06 70 7F" + Protocols.Elm327.EndOfLine +
                "59 04 01 3F" + Protocols.Elm327.EndOfLine +
                "59 27 71 21" + Protocols.Elm327.EndOfLine +
                "59 00 00 13" + Protocols.Elm327.EndOfLine + Protocols.Elm327.Prompt;
        }
    }
}
