using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Extensions;
using OBD2.Cables;

namespace OBD2.SpecificPids
{
    public class Mode9 : ParameterIdentification
    {

        /// <summary>
        /// Instantiates a new instance of the Elm327 Mode9 class. This class should not be used on its own.
        /// </summary>
        public Mode9() :
            base("Diagnostic Trouble Code Request", 0x09, 0x02, 0x01, "", "", "DTC", "Diagnostic")
        {
            // do nothing
        }

        public override byte PacketSize { get { return 0x01; } }

        public override string Pack()
        {
            return Mode.ToString("X2") + PID.ToString("X2") + OBD2.Protocols.Elm327.EndOfLine;
        }

        public string RequestVIN(OBD2.Cables.Cable cable)
        {
            string dataStr = cable.Communicate(this);
            string[] dataLines = ParameterIdentification.PrepareResponseString(dataStr);
            string vin = string.Empty;

            foreach (string line in dataLines)
            {
                byte[] dataBytes = ParameterIdentification.ParseStringValues(line);
                if (dataBytes.Length >= 7)
                {
                    byte receivedMode = (byte)(dataBytes[0] - 0x40);
                    if (receivedMode == this.Mode)
                    {
                        // first byte is the mode, second the pid, third the line number,
                        // and fourth or index 3 is the start of vin data
                        for (int i = 3; i < dataBytes.Length; i++)
                        {
                            if (dataBytes[i] != 0)
                            {
                                vin += Convert.ToChar(dataBytes[i]);
                            }
                        }
                    }
                    else
                    {
                        Diagnostics.DiagnosticLogger.Log("Cannot decode VIN, expected mode " + this.Mode + " and received " + receivedMode);
                        return null;
                    }
                }
                else
                {
                    Diagnostics.DiagnosticLogger.Log("Cannot decode VIN, expected at least 7 characters in line \"" + line + "\" and received " + dataBytes.Length);
                    return null;
                }
            }

            return vin;
        }

        public override string SimulatedResponse()
        {
            return
                "49 02 01 00 00 00 31" + Protocols.Elm327.EndOfLine +
                "49 02 02 47 43 48 4B" + Protocols.Elm327.EndOfLine +
                "49 02 03 33 33 32 38" + Protocols.Elm327.EndOfLine +
                "49 02 04 37 31 34 32" + Protocols.Elm327.EndOfLine +
                "49 02 05 30 38 32 36" + Protocols.Elm327.EndOfLine +
                Protocols.Elm327.Prompt;
        }
    }
}
