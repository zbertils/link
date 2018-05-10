using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Xml.Serialization;

using Extensions;

namespace OBD2
{
    [DataContract]
    public class ParameterIdentification
    {
        public static class ResponseByteOffsets
        {
            public const byte Mode = 0;

            // offsets for normal, non-extended pids
            public const byte PID = 1;
            public const byte DataByte0 = 2;
            public const byte DataByte1 = 3;
            public const byte DataByte2 = 4;
            public const byte DataByte3 = 5;
            public const byte DataByte4 = 6;

            // offsets for extended pids
            public const byte ExtendedPIDByte0 = 1;
            public const byte ExtendedPIDByte1 = 2;
            public const byte ExtendedDataByte0 = 3;
            public const byte ExtendedDataByte1 = 4;
            public const byte ExtendedDataByte2 = 5;
            public const byte ExtendedDataByte3 = 6;
        }

        public static class PacketSizes
        {
            public const byte Normal = 2;
            public const byte Extended = 3;
        }

        // properties that may be read only by the user
        [DataMember] public string FormulaString            { get; protected set; }
        [DataMember] public string Name                     { get; protected set; }
        [DataMember] public string Description              { get; protected set; }
        [DataMember] public string Units                    { get; protected set; }
        [DataMember] public string Group                    { get; protected set; }
        [DataMember] public string PidType                  { get; protected set; }
        [DataMember] public byte Mode                       { get; protected set; }
        [DataMember] public short PID                       { get; protected set; }
        [DataMember] public byte DataByteCount              { get; protected set; }

        // properties that may be edited freely by the user
        [DataMember] public bool LogThisPID                 { get; set; }

        /// <summary>
        /// The header to use for J1850 PIDs. This value is not necessary to have for protocols outside of J1850.
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Header { get; set; }

        [IgnoreDataMember]
        public long Timestamp { get; protected set; }

        [IgnoreDataMember]
        public double LastDecodedValue
        {
            get
            {
                return _lastdecodedValue;
            }
            set
            {
                this.Timestamp = DateTime.Now.Ticks;
                _lastdecodedValue = value;
            }
        }
        private double _lastdecodedValue = double.NaN;

        public ParameterIdentification(
            string name,
            byte mode,
            short pid,
            byte dataByteCount,
            string formulaString,
            string units,
            string group,
            string pidType,
            string description = "",
            string header = "")
        {
            this.Name = name;
            this.Mode = mode;
            this.PID = pid;
            this.DataByteCount = dataByteCount;
            this.FormulaString = formulaString;
            this.Units = units;
            this.Group = group;
            this.PidType = pidType;
            this.Description = description;
            this.Header = header;
        }

        [IgnoreDataMember]
        public virtual byte PacketSize
        {
            get
            {
                if (this.PID <= byte.MaxValue &&
                    this.Mode <= 2)
                {
                    return PacketSizes.Normal;
                }
                else
                {
                    return PacketSizes.Extended;
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                "Name:          {0}" + System.Environment.NewLine +
                "Mode:          {1}" + System.Environment.NewLine +
                "PID:           {2}" + System.Environment.NewLine +
                "DataByteCount: {3}" + System.Environment.NewLine +
                "FormulaString: {4}" + System.Environment.NewLine +
                "Units:         {5}" + System.Environment.NewLine +
                "Group:         {6}" + System.Environment.NewLine +
                "PidType:       {7}" + System.Environment.NewLine +
                "Description:   {8}" + System.Environment.NewLine +
                "Header:        {9}",
                this.Name,
                this.Mode,
                this.PID,
                this.DataByteCount,
                this.FormulaString,
                this.Units,
                this.Group,
                this.PidType,
                this.Description,
                this.Header);
        }


        /// <summary>
        /// Packetizes the ParameterIdentification object.
        /// </summary>
        /// <param name="pid"> The ParameterIdentification object to packetize. </param>
        /// <returns> The byte[] object representing the packet to send across the OBD2 connection. </returns>
        public virtual string Pack()
        {
            string dataStr = string.Empty;

            // if the packet size is 3 or more the byte order needs to be reversed
            dataStr = string.Format("{0} {1}", // no space for end of line char
                this.Mode.ToString(Protocols.ToHexFormat),
                this.PID.ToString(Protocols.ToHexFormat));

            // a small optimization, if there are four or less data bytes being
            // returned then it can fit into one frame and the cable can return
            // immediately after receiving that one frame, otherwise just let
            // the cable timeout on reads in case it is a special PID like mode 3
            if (this.DataByteCount <= 4 &&
                //pid.Header != OBD2.Protocols.J1850.Headers.Default)
                this.Mode == 0x22)
            {
                dataStr += " 01";
            }

            dataStr += OBD2.Protocols.Elm327.EndOfLine;

            return dataStr;
        }

        /// <summary>
        /// Unpacks response data to the given pid.
        /// </summary>
        /// <param name="data"> The response to communicating <paramref name="pid"/> to the OBD2 cable. </param>
        /// <param name="pid"> The ParameterIdentification expected to match <paramref name="data"/>. </param>
        /// <returns> The decoded value if successful, and double.NaN otherwise. </returns>
        public virtual double Unpack(string data)
        {
            try
            {
                // if there are not enough bytes to decode the message than return an empty string
                if (string.IsNullOrEmpty(data))
                {
                    return double.NaN;
                }

                // convert back to a string so the ascii values can be parsed into their integer representations,
                // then parse into individual integers, for the basic pid values there will only be one line returned,
                // for all other multi-line pids they are expected to override the Unpack() function and handle the parsing individually
                string[] dataStr = PrepareResponseString(data);
                if (dataStr.Length > 0)
                {
                    byte[] values = ParseStringValues(dataStr[0]);

                    // default the respond pid to be if not using extended pids,
                    // then figure out if it should be adjusted if it is an extended pid
                    short responsePid = values[ParameterIdentification.ResponseByteOffsets.PID];
                    if (this.PacketSize == 3)
                    {
                        responsePid = BitConverter.ToInt16(new byte[] { values[ParameterIdentification.ResponseByteOffsets.ExtendedPIDByte1], values[ParameterIdentification.ResponseByteOffsets.ExtendedPIDByte0] }, 0);
                    }

                    // make sure this response has the correct number of expected data bytes,
                    // the correct expected mode, and the correct expected pid
                    if (values[ParameterIdentification.ResponseByteOffsets.Mode] - 0x40 != this.Mode ||
                        responsePid != this.PID)
                    {
                        return double.NaN;
                    }

                    // copy the response data byte values to a new array
                    // so it can be formatted in to the formula string
                    object[] responseValues = new object[this.DataByteCount];
                    if (this.PacketSize == 3)
                    {
                        Array.Copy(values, ParameterIdentification.ResponseByteOffsets.ExtendedDataByte0, responseValues, 0, this.DataByteCount);
                    }
                    else
                    {
                        Array.Copy(values, ParameterIdentification.ResponseByteOffsets.DataByte0, responseValues, 0, this.DataByteCount);
                    }

                    string responseExpression = string.Format(this.FormulaString, responseValues);
                    return Evaluate(responseExpression);
                }
            }
            catch (Exception ex)
            {
                Diagnostics.DiagnosticLogger.Log("Could not decode PID " + this.ToString() + "." + System.Environment.NewLine + "received: " + data, ex);
            }

            return double.NaN;
        }

        /// <summary>
        /// Removes any ELM specific characters that do not relate to a request.
        /// </summary>
        /// <param name="dataStr"> The data string that was sent from the cable. </param>
        /// <returns> The prepared line without prompt characters or end of line characters. </returns>
        public static string[] PrepareResponseString(string dataStr)
        {
            if (!string.IsNullOrEmpty(dataStr))
            {
                dataStr.Trim(); // remove leading and trailing spaces
                dataStr = dataStr.Replace(OBD2.Protocols.Elm327.Prompt, string.Empty);
                return dataStr.Split(new char[] { OBD2.Protocols.Elm327.EndOfLineChar }, StringSplitOptions.RemoveEmptyEntries);
            }

            return null;
        }

        /// <summary>
        /// Parses string hex values into individual bytes.
        /// </summary>
        /// <param name="elm327Response"> The ELM response to a request. Assumed to be space-separated for each byte. </param>
        /// <returns> The array containing parsed values. </returns>
        public static byte[] ParseStringValues(string elm327Response)
        {
            if (string.IsNullOrEmpty(elm327Response))
            {
                return null;
            }

            // split the values based on spaces
            string[] dataStrValues = elm327Response.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] values = new byte[dataStrValues.Length];

            for (int i = 0; i < dataStrValues.Length; i++)
            {
                try
                {
                    // parse everything from hex as a uint, there are no signed values with obd2
                    values[i] = byte.Parse(dataStrValues[i], System.Globalization.NumberStyles.HexNumber);
                }
                catch (Exception ex)
                {
                    Diagnostics.DiagnosticLogger.Log("Invalid conversion of string to integer: " + dataStrValues[i], ex);
                    return null;
                }
            }

            return values;
        }

        protected String[] prepMarkedLines(String[] preppedLines)
        {

            String identifier = ":";

            // determine if the array of strings contains any lines in the format "X: hh hh hh..."
            bool linesAreMarked = false;
            foreach (String line in preppedLines)
            {
                if (!string.IsNullOrEmpty(line) && line.Length > 2)
                {
                    if (line.Contains(identifier))
                    {
                        // line contains an identifier, assume all lines are marked
                        linesAreMarked = true;
                        break;
                    }
                }
            }

            // if the lines are marked, remove any that do not have an identifier,
            // some bad elm adapters have buffer issues when receiving multiple lines
            // and put a "no dtc reported" line first that is not marked with an identifier
            if (linesAreMarked)
            {
                List<String> keptLines = new List<String>();
                foreach (String line in preppedLines)
                {
                    if (line.Contains(identifier))
                    {
                        // only keep the line if it has an identifier
                        keptLines.Add(line);
                    }
                }

                // go through each kept line and put them together as a single line,
                // this is because some adapters put bytes out of order with an odd number per line,
                // but the values as a whole make sense and add up to the correct number of bytes
                String finalLine = "";
                foreach (String line in keptLines)
                {

                    // if the final line is not empty, meaning there are leading characters, then put a space so the parser can still separate them
                    if (!string.IsNullOrEmpty(finalLine))
                    {
                        finalLine += " ";
                    }

                    int index = line.IndexOf(identifier);
                    String subStr = line.Substring(index + 1);

                    // the lines should already be in order, append to the final line in the order of the list
                    finalLine += subStr.Trim();
                }

                return new String[] { finalLine.Trim() };
            }
            else
            {
                // lines are not marked, assume each line is in the format "43 <optional count> hh hh hh..."
                return preppedLines;
            }
        }

        /// <summary>
        /// Evaluates the given string to a double.
        /// </summary>
        /// <param name="expression"> The expression to evaluate. </param>
        /// <returns> The double representation of the evaluated string. </returns>
        protected double Evaluate(string expression)
        {
            double returnValue = double.NaN;

            try
            {
                DataTable dataTable = new DataTable();
                DataColumn dataColumn = new DataColumn("Eval", typeof(double), expression);
                dataTable.Columns.Add(dataColumn);
                dataTable.Rows.Add(0); // add a row so the expression will be evaluated and updated
                returnValue = (double)(dataTable.Rows[0]["Eval"]);
            }
            catch (Exception ex)
            {
                returnValue = double.NaN;
                Diagnostics.DiagnosticLogger.Log("Could not evaluate PID using expression \"" + expression + "\".", ex);
            }

            return returnValue;
        }

        public virtual string SimulatedResponse(Protocols.Protocol type)
        {
            // populate the string with valid info until the data packets, then fill with random data
            string dataStr = (this.Mode + 0x40).ToString("X2") + " ";

            // the pid needs to be split into two separate bytes if an extended pid
            // with a space separating them just like the elm response would be
            if (this.PacketSize == 3)
            {
                byte[] pidBytes = BitConverter.GetBytes(this.PID);
                dataStr += pidBytes[1].ToString("X2") + " " + pidBytes[0].ToString("X2");
            }
            else
            {
                dataStr += this.PID.ToString("X2");
            }

            for (int i = 0; i < this.DataByteCount; i++)
            {
                dataStr += " " + ((byte)(DateTime.Now.Ticks + this.PID)).ToString("X2");
            }


            if (type == Protocols.Protocol.HighSpeedCAN11 ||
                type == Protocols.Protocol.LowSpeedCAN11 ||
                type == Protocols.Protocol.HighSpeedCAN29 ||
                type == Protocols.Protocol.LowSpeedCAN29)
            {
                dataStr += " AA AA AA AA";
            }

            // purposely sleep to simulate the minimum cable transmission delay
            System.Threading.Thread.Sleep(25);

            return dataStr;
        }

    }
}
