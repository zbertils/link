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

        public override string SimulatedResponse()
        {
            // this simulation is for ISO based protocols only, CAN may or may not
            // have an extra byte after the mode describing how many codes there are
            return "43 01 76 02 56 45 10" + Protocols.Elm327.EndOfLine + Protocols.Elm327.Prompt;
        }

    }
}
