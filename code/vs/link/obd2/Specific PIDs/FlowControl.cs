using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD2.SpecificPids
{
    public class FlowControl : ParameterIdentification
    {

        public FlowControl() :
            base("Flow Control", 0x00, 0x00, 0x00, "{0}", "N/A", "DTC", "Request", "Flow control for DTC request")
        {
            // do nothing for now
        }

        public override byte PacketSize { get { return 0x30; } }

    }
}
 