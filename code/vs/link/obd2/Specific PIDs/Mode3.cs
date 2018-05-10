using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Extensions;
using OBD2.Cables;

namespace OBD2.SpecificPids
{
    public class Mode3 : DtcParameterIdentification
    {

        /// <summary>
        /// Instantiates a new instance of the Elm327 Mode3 class. This class should not be used on its own.
        /// </summary>
        public Mode3() :
            base(DiagnosticTroubleCode.CodeType.Permanent, "Diagnostic Trouble Code Request", 0x03, 0x55, 0x01, "", "", "DTC", "Diagnostic")
        {
            // do nothing
        }

        public override byte PacketSize { get { return 0x01; } }

        public override string SimulatedResponse(Protocols.Protocol type)
        {
            if (type == Protocols.Protocol.J1850)
            {
                // this simulation is for ISO based protocols only, CAN may or may not
                // have an extra byte after the mode describing how many codes there are
                return "43 01 76 02 56 45 10" + Protocols.Elm327.EndOfLine + Protocols.Elm327.Prompt;
            }
            else
            {

                // simulation for CAN related vehicles with only one packets-worth of codes
                //        return "43 03 01 76 02 56 45 10";

                // simulation for CAN related to vehicles with multiple packets-worth of codes
                // where each line is marked with an identifier
                return "43 00 AA AA AA AA AA " + Protocols.Elm327.EndOfLine + // purposeful bad line
                    "010 " + Protocols.Elm327.EndOfLine + // purposeful bad line
                    "0: 43 07 00 97 01 02 " + Protocols.Elm327.EndOfLine + // start of trouble codes
                    "1: 01 13 11 01 11 C2 22 " + Protocols.Elm327.EndOfLine +
                    " 2: 27 22 28 AA AA AA AA" + Protocols.Elm327.EndOfLine + Protocols.Elm327.Prompt;
            }
        }

    }
}
