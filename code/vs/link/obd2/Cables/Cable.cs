using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Ports;

namespace OBD2.Cables
{
    public abstract class Cable : IDisposable
    {
        public class CableInfo
        {
            public string Version;
            public bool AutoProtocolSet;
            public OBD2.Protocols.Protocol Protocol;
            public bool EchoOff;
            public string Description;

            /// <summary>
            /// Creates a default CableInfo object.
            /// </summary>
            public CableInfo()
            {
                Version = string.Empty;
                AutoProtocolSet = false;
                Protocol = OBD2.Protocols.Protocol.None;
                EchoOff = false;
                Description = string.Empty;
            }
        }

        public delegate void ConnectionCallback(int step, CableInfo info);

        public enum Type
        {
            PassThrough,
            Elm327,
            Simulated
        }


        public Type CableType { get; protected set; }

        /// <summary>
        /// How many bytes have been sent to the cable.
        /// </summary>
        public long BytesSent { get; protected set; }

        /// <summary>
        /// How many bytes have been received from the cable.
        /// </summary>
        public long BytesReceived { get; protected set; }

        /// <summary>
        /// Gets a value indicating the open or closed status of the cable connection. 
        /// </summary>
        /// <returns> True if the serial port is open; otherwise, false. The default is false. </returns>
        public virtual bool IsOpen { get { return cableConnection.IsOpen; } }

        public virtual bool IsInitialized { get; protected set; }

        public OBD2.Protocols.Protocol Protocol { get; protected set; }

        public List<Utilities.IniFileEntry> TroubleCodeDescriptions { get; set; }

        protected string currentJ1850Header = string.Empty;

        /// <summary>
        /// Creates a new instance of Cable.
        /// </summary>
        /// <param name="timeoutMilliseconds"> The timeout to give </param>
        protected Cable(string port, int timeoutMilliseconds, int baudRate) :
            base() // make sure the cable type and trouble code descriptions have valid starting values
        {
            this.cableConnection = new SerialPort(port, baudRate);
            this.timeoutMilliseconds = timeoutMilliseconds;
            BytesSent = 0;
            BytesReceived = 0;

            // try opening the port if it is not already open
            if (!cableConnection.IsOpen)
            {
                cableConnection.Open();
            }
        }

        /// <summary>
        /// The constructor used by simulated cables and setting defaults for the base class.
        /// </summary>
        protected Cable()
        {
            CableType = Type.PassThrough; // default to being a pass-through cable
            TroubleCodeDescriptions = new List<Utilities.IniFileEntry>();
        }

        protected void UpdateConnectionStatus(int step, CableInfo info, ConnectionCallback callback)
        {
            if (callback != null)
            {
                callback(step, info);
                System.Threading.Thread.Sleep(100);
            }
        }

        /// <summary>
        /// Closes and disposes the cable connection.
        /// </summary>
        /// <remarks>
        /// Once this function is called the cable instance can no longer be used.
        /// </remarks>
        public void Close()
        {
            if (cableConnection != null)
            {
                cableConnection.Close();
                cableConnection.Dispose();
                cableConnection = null;
            }
        }

        public void Dispose()
        {
            Close();
        }
        
        /// <summary>
        /// Communicates a parameter identification to the cable.
        /// </summary>
        /// <param name="pid"> The ParameterIdentification object to communicate to the cable. </param>
        /// <returns> The response if one is expected, and null otherwise. </returns>
        public abstract string Communicate(ParameterIdentification pid);

        /// <summary>
        /// Sends the given data bytes through the cable.
        /// </summary>
        /// <param name="pid"> The ParameterIdentification object to send. </param>
        /// <returns> True if the data was sent successfully and false otherwise. </returns>
        public abstract bool Send(ParameterIdentification pid);

        /// <summary>
        /// Sends the given data bytes through the cable.
        /// </summary>
        /// <param name="data"> The data to send to the cable. </param>
        /// <returns> True if the data was sent successfully and false otherwise. </returns>
        public abstract bool Send(string data);

        /// <summary>
        /// Receives data from the cable.
        /// </summary>
        /// <param name="timeoutMilliseconds"> The timeout to use instead of the one passed through the constructor. </param>
        /// <returns> The response if one is expected, and null otherwise. </returns>
        public abstract string Receive(int timeoutMilliseconds = 75);

        /// <summary>
        /// Requests the PCM clears trouble codes from the vehicle.
        /// </summary>
        public abstract void ClearTroubleCodes();

        /// <summary>
        /// Requests diagnostic trouble codes from the vehicle.
        /// </summary>
        /// <returns> The list of diagnostic trouble codes as strings, e.g. "P0176". </returns>
        public abstract List<DiagnosticTroubleCode> RequestTroubleCodes();

        /// <summary>
        /// Requests diagnostic trouble code statuses from the vehicle.
        /// </summary>
        /// <returns> The list of diagnostic trouble code and their associated statuses. </returns>
        public abstract List<Tuple<DiagnosticTroubleCode, string>> RequestAllDtcStatuses();

        /// <summary>
        /// Requests the VIN from the ECU.
        /// </summary>
        /// <returns> The VIN as a string. </returns>
        public abstract string RequestVIN();


        protected SerialPort cableConnection;
        protected int timeoutMilliseconds;

    }
}
