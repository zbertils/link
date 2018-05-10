using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBD2.Cables
{
    public class Elm327CableSimulator : Elm327Cable
    {
        /// <summary>
        /// Gets if the cable connection is open or not.
        /// </summary>
        public override bool IsOpen { get { return IsInitialized; } }

        /// <summary>
        /// True if the simulator should return trouble codes and false otherwise. The default is true.
        /// </summary>
        public bool SimulateTroubleCodes = true;

        /// <summary>
        /// Creates a new instance of CustomCable.
        /// </summary>
        /// <param name="port"> The port the cable is connected to. </param>
        /// <param name="timeoutMilliseconds"> The timeout to use for communication with the cable. </param>
        public Elm327CableSimulator(string port, int timeoutMilliseconds, Elm327Cable.ConnectionCallback callback) :
            base()
        {
            CableType = Type.Simulated;
            IsInitialized = false; // default to false

            // default cable info
            CableInfo info = new Elm327Cable.CableInfo();

            info.Description = "Discovering ELM version";
            UpdateConnectionStatus(1, info, callback);

            // detect what type of cable is connected
            string response = "ELM327 v1.5 Simulated";
            if (response.Contains(Protocols.Elm327.Header))
            {
                Diagnostics.DiagnosticLogger.Log("Cable is ELM327 type");

                string version = "NA";

                // get the version number for posterity
                if (response.Contains("v"))
                {
                    int indexOfVersion = response.IndexOf("v");
                    version = response.Substring(indexOfVersion);
                    Diagnostics.DiagnosticLogger.Log("Discovered ELM version: " + version);

                    info.Description = "Discovered ELM version: " + version;
                    info.Version = version;
                    UpdateConnectionStatus(1, info, callback);
                }

                // turn echo off
                Diagnostics.DiagnosticLogger.Log("Turning echo off");
                info.Description = "Turning echo off";
                UpdateConnectionStatus(1, info, callback);

                response = "OK";
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not turn echo off");
                    return;
                }

                info.EchoOff = true;
                info.Description = "Turning auto protocol on";
                UpdateConnectionStatus(2, info, callback);

                Diagnostics.DiagnosticLogger.Log("Turning auto protocol on");
                response = "OK";
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not set protocol to Auto");
                    return;
                }

                info.Description = "Forcing search for vehicle's protocol";
                UpdateConnectionStatus(2, info, callback);

                Diagnostics.DiagnosticLogger.Log("Forcing a search for existing protocol");
                response = "SEARCHING...";
                if (string.IsNullOrEmpty(response))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not force an auto protocol search");
                    info.Description = "Error, could not force an auto protocol search";
                    UpdateConnectionStatus(2, info, callback);
                    return;
                }

                response = "AUTO,J1850";
                string chosenProtocol = response.Replace(Protocols.Elm327.Responses.Auto, string.Empty).Replace(",", string.Empty).Trim();
                Diagnostics.DiagnosticLogger.Log("Protocol chosen: " + chosenProtocol);
                if (!response.Contains(Protocols.Elm327.Responses.Auto))
                {
                    Diagnostics.DiagnosticLogger.Log("Displayed protocol did not mention auto");
                    info.Description = "Error, displayed protocol did not mention auto";
                    UpdateConnectionStatus(2, info, callback);
                    return;
                }

                Protocol = OBD2.Protocols.NameToProtocol(chosenProtocol);
                info.Protocol = Protocol;
                info.AutoProtocolSet = true;
                info.Description = "Protocol has been chosen";
                UpdateConnectionStatus(3, info, callback);

                // everything is good to go
                IsInitialized = true;

                // fully initialized, the fourth step is the final step
                info.Description = "Connected!";
                UpdateConnectionStatus(4, info, callback);
            }
        }

        protected override string SendElmInitString(string data, int sleepMilliseconds = 500)
        {
            return Protocols.Elm327.Responses.OK;
        }

        public override string Communicate(ParameterIdentification pid)
        {
            // check if the header needs to be set
            if (pid.Header != currentJ1850Header &&
                !string.IsNullOrEmpty(pid.Header))
            {
                string response = SendElmInitString(Protocols.Elm327.SetFrameHeader(pid.Header));
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not set frame header for PID" + System.Environment.NewLine + pid.ToString());
                    return null;
                }
            }
            else if (string.IsNullOrEmpty(pid.Header))
            {
                string response = SendElmInitString(Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default));
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not set default frame header for PID" + System.Environment.NewLine + pid.ToString());
                    return null;
                }
            }

            return pid.SimulatedResponse();
        }

        public override List<DiagnosticTroubleCode> RequestTroubleCodes()
        {
            if (SimulateTroubleCodes)
            {
                return base.RequestTroubleCodes();
            }
            else
            {
                return new List<DiagnosticTroubleCode>();
            }
        }

    }
}
