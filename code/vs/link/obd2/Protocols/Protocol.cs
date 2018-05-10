using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD2
{
    public static class Protocols
    {
        public const string ToHexFormat = "X2";

        public enum Protocol
        {
            None,
            Auto,
            J1850,
            HighSpeedCAN,
            LowSpeedCAN,
            VPW
        }

        public static Protocol NameToProtocol(string name)
        {
            if (name.ToUpper().Contains("J1850"))
            {
                return Protocol.J1850;
            }
            else if (name.ToUpper().Contains("AUTO"))
            {
                return Protocol.Auto;
            }
            else if (name.ToUpper().Contains("CAN"))
            {
                if (name.ToUpper().Contains("HIGH"))
                {
                    return Protocol.HighSpeedCAN;
                }
                else
                {
                    return Protocol.LowSpeedCAN;
                }
            }
            else if (name.ToUpper().Contains("VPW"))
            {
                return Protocol.VPW;
            }

            return Protocol.None;
        }

        public static class J1850
        {
            public static class Headers
            {
                public static class PriorityAndType
                {
                    public const string LowPriorityFunctional = "68";
                    public const string LowPriorityNodeToNode = "6C";

                    public const string Default = LowPriorityFunctional;
                }

                public static class Destinations
                {
                    public const string RequestLegislatedDiagnostics = "6A";
                    public const string Engine = "10";
                    public const string Transmission = "18";
                    public const string Body = "40";
                    public const string AirBag = "58";
                    public const string NULL = "FF";

                    public const string Default = RequestLegislatedDiagnostics;
                }

                public static class Sources
                {
                    public const string OffBoardCable = "F1";

                    public const string Default = OffBoardCable;
                }

                public const string Default = PriorityAndType.Default               + " " + Destinations.Default        + " " + Sources.Default;
                public const string PCM     = PriorityAndType.LowPriorityNodeToNode + " " + Destinations.Engine         + " " + Sources.OffBoardCable;
                public const string TCM     = PriorityAndType.LowPriorityNodeToNode + " " + Destinations.Transmission   + " " + Sources.OffBoardCable;
                public const string BCM     = PriorityAndType.LowPriorityNodeToNode + " " + Destinations.Body           + " " + Sources.OffBoardCable;
                public const string AirBag  = PriorityAndType.LowPriorityNodeToNode + " " + Destinations.AirBag         + " " + Sources.OffBoardCable;

                public const string NULL    = PriorityAndType.LowPriorityNodeToNode + " " + Destinations.NULL           + " " + Sources.OffBoardCable;
            }

        }

        public static class Elm327
        {

            public const string Header = "ELM327";
            public const string Reset = "AT Z";
            public const string DisplayProtocol = "AT DP";
            public const string SetAutoProtocol = "AT SP 0";
            public const string EchoOff = "AT E0";
            public const string EchoOn = "AT E1";
            public const string Prompt = ">";

            /// <summary>
            /// The command for setting the frame header. This string needs to be given the 3-byte frame header value, e.g. "6C 10 F1" or a value from Protocols.J1850.Headers.
            /// </summary>
            public static string SetFrameHeader(string header)
            {
                return "AT SH " + header;
            }

            /// <summary>
            /// Forces a protocol search after SetAutoProtocol has been set.
            /// </summary>
            public const string ForceProtocolSearch = "01 01";

            public const string EndOfLine = "\r";
            public const char EndOfLineChar = '\r';
            public const byte EndOfLineByte = 0x0D;

            public static class Responses
            {
                public const string Auto = "AUTO";
                public const string OK = "OK";
                public const string Error = "NO DATA";
                public const string EndOfLine = "\r";
                public const string Searching = "SEARCHING...";
                public const string Stopped = "STOPPED";
            }

        }

    }
}