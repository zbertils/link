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
            HighSpeedCAN11,
            LowSpeedCAN11,
            HighSpeedCAN29,
            LowSpeedCAN29,
            VPW,
            Unknown
        }

        public static Protocol NameToProtocol(String name)
        {
            String upperName = name.ToUpper();
            if (upperName.Contains("J1850"))
            {
                return Protocol.J1850;
            }
            else if (upperName.Contains("AUTO"))
            {
                return Protocol.Auto;
            }
            else if (upperName.Contains("CAN"))
            {
                if (upperName.Contains("11/500"))
                {
                    return Protocol.HighSpeedCAN11;
                }
                else if (upperName.Contains("11/250"))
                {
                    return Protocol.LowSpeedCAN11;
                }
                else if (upperName.Contains("29/500"))
                {
                    return Protocol.HighSpeedCAN29;
                }
                else if (upperName.Contains("29/250"))
                {
                    return Protocol.LowSpeedCAN29;
                }
                else
                {
                    return Protocol.Unknown;
                }
            }
            else if (upperName.Contains("VPW"))
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

        public static class CAN
        {
            public static class Headers
            {

                public static class Destinations
                {
                    public const String OffBoardCable = "D";
                }

                public static class Sources
                {
                    public const String PCM = "8";
                    public const String All = "F";
                }

                public const String Default = "7" + Destinations.OffBoardCable + Sources.All;
            }
        }

        public static class Elm327
        {

            public const String Header = "ELM327";
            public const String Reset = "AT Z";
            public const String DisplayProtocol = "AT DP";
            public const String SetAutoProtocol = "AT SP 0";
            public const String SetTimeoutMaximum = "AT ST FF";
            public const String SetSpacesOff = "AT S0";
            public const String EchoOff = "AT E0";
            public const String EchoOn = "AT E1";
            public const String AdaptiveTimingOn = "AT AT1";
            public const String AdaptiveTimingOff = "AT AT0";
            public const String Prompt = ">";

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
                public const string NoData = "NO DATA";
                public const string EndOfLine = "\r";
                public const string Searching = "SEARCHING...";
                public const string Stopped = "STOPPED";
            }

        }

    }
}