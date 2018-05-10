using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Extensions;
using OBD2.Cables;

namespace OBD2.SpecificPids
{
    public class Mode7 : DtcParameterIdentification
    {

        /// <summary>
        /// Instantiates a new instance of the Elm327 Mode7 class. This class should not be used on its own.
        /// </summary>
        public Mode7() :
            base(DiagnosticTroubleCode.CodeType.Pending, "Diagnostic Trouble Code Request", 0x07, 0x55, 0x01, "", "", "DTC", "Diagnostic")
        {
            // do nothing
        }

        public override byte PacketSize { get { return 0x01; } }

        public override string SimulatedResponse()
        {
            // this simulation is for ISO based protocols only, CAN may or may not
            // have an extra byte after the mode describing how many codes there are
            return "47 02 76 96 45" + Protocols.Elm327.EndOfLine + Protocols.Elm327.Prompt;
        }
    }
}
