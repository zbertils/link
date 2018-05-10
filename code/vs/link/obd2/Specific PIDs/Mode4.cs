using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Extensions;
using OBD2.Cables;

namespace OBD2.SpecificPids
{
    public class Mode4 : DtcParameterIdentification
    {

        /// <summary>
        /// Instantiates a new instance of the Elm327 Mode4 class. This class should not be used on its own.
        /// </summary>
        public Mode4() :
            base(DiagnosticTroubleCode.CodeType.Permanent, "Diagnostic Trouble Code Clear", 0x04, 0x55, 0x01, "", "", "DTC", "Diagnostic")
        {
            // do nothing
        }

        public override byte PacketSize { get { return 0x01; } }

        public override string SimulatedResponse()
        {
            return Protocols.Elm327.Prompt; // there is nothing to return
        }

    }
}
