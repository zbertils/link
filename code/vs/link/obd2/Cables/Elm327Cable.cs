using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace OBD2.Cables
{
    public class Elm327Cable : Cable
    {

        private string lastFrameHeader = string.Empty;
        protected OBD2.SpecificPids.Mode3 mode3 = new SpecificPids.Mode3();
        protected OBD2.SpecificPids.Mode4 mode4 = new SpecificPids.Mode4();
        protected OBD2.SpecificPids.Mode7 mode7 = new SpecificPids.Mode7();
        protected OBD2.SpecificPids.Mode9 mode9 = new SpecificPids.Mode9();
        protected OBD2.SpecificPids.ModeA modeA = new SpecificPids.ModeA();
        protected OBD2.SpecificPids.Mode19 mode19 = new SpecificPids.Mode19();

        /// <summary>
        /// Constructor for simulated cables.
        /// </summary>
        protected Elm327Cable() :
            base()
        {
            // do nothing
            CableType = Type.Elm327;
        }

        /// <summary>
        /// Creates a new instance of CustomCable.
        /// </summary>
        /// <param name="port"> The port the cable is connected to. </param>
        /// <param name="timeoutMilliseconds"> The timeout to use for communication with the cable. </param>
        public Elm327Cable(string port, int timeoutMilliseconds, ConnectionCallback callback) :
            base(port, timeoutMilliseconds, 38400)
        {
            CableType = Type.Elm327;
            IsInitialized = false; // default to false

            // default cable info
            CableInfo info = new CableInfo();

            // set the newline character to be what elm327 uses
            cableConnection.NewLine = Protocols.Elm327.EndOfLine;

            info.Description = "Discovering ELM version";
            UpdateConnectionStatus(1, info, callback);

            // detect what type of cable is connected
            string response = SendCommand(Protocols.Elm327.Reset, 1000);
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

                response = SendCommand(Protocols.Elm327.EchoOff, 10000);
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not turn echo off");
                    return;
                }

                info.EchoOff = true;
                info.Description = "Turning auto protocol on";
                UpdateConnectionStatus(2, info, callback);

                Diagnostics.DiagnosticLogger.Log("Turning auto protocol on");
                response = SendCommand(Protocols.Elm327.SetAutoProtocol, 10000);
                if (!response.Contains(Protocols.Elm327.Responses.OK))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not set protocol to Auto");
                    return;
                }

                info.Description = "Forcing search for vehicle's protocol";
                UpdateConnectionStatus(2, info, callback);

                Diagnostics.DiagnosticLogger.Log("Forcing a search for existing protocol");
                response = SendCommand(Protocols.Elm327.ForceProtocolSearch, 10000);
                if (string.IsNullOrEmpty(response))
                {
                    Diagnostics.DiagnosticLogger.Log("Could not force an auto protocol search");
                    info.Description = "Error, could not force an auto protocol search";
                    UpdateConnectionStatus(2, info, callback);
                    return;
                }

                response = SendCommand(Protocols.Elm327.DisplayProtocol, 10000);
                string chosenProtocol = response.Replace(Protocols.Elm327.Responses.Auto, string.Empty).Replace(",", string.Empty).Trim();
                Diagnostics.DiagnosticLogger.Log("Protocol chosen: " + chosenProtocol);
                if (!response.Contains(Protocols.Elm327.Responses.Auto))
                {
                    Diagnostics.DiagnosticLogger.Log("Displayed protocol did not mention auto");
                    info.Description = "Error, displayed protocol did not mention auto";
                    UpdateConnectionStatus(2, info, callback);
                    //return;
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

        protected virtual string SendCommand(string data, int sleepMilliseconds = 500)
        {
            try
            {
                cableConnection.ReadTimeout = sleepMilliseconds;

                cableConnection.WriteLine(data);
                System.Threading.Thread.Sleep(sleepMilliseconds);
                string response = cableConnection.ReadExisting();

                response = response.Replace(Protocols.Elm327.Prompt, string.Empty); // remove the prompt character before returning the response
                Diagnostics.DiagnosticLogger.Log("ELM327 response: " + response);

                return response;
            }
            catch (TimeoutException)
            {
                return string.Empty;
            }
        }

        public override string Communicate(ParameterIdentification pid)
        {
            // only the J1850 protocol needs to set the header,
            // CAN and others do not have the same set-header commands
            if (Protocol == Protocols.Protocol.J1850)
            {
                // check if the header needs to be set
                if (//pid.Header != currentJ1850Header &&
                    !string.IsNullOrEmpty(pid.Header))
                {
                    if (lastFrameHeader != pid.Header)
                    {
                        string response = SendCommand(Protocols.Elm327.SetFrameHeader(pid.Header));
                        if (!response.Contains(Protocols.Elm327.Responses.OK))
                        {
                            Diagnostics.DiagnosticLogger.Log("Could not set frame header for PID" + System.Environment.NewLine + pid.ToString());
                            return null;
                        }

                        lastFrameHeader = pid.Header;
                    }
                }
                else if (string.IsNullOrEmpty(pid.Header))
                {
                    if (lastFrameHeader != Protocols.J1850.Headers.Default)
                    {
                        string response = SendCommand(Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default));
                        if (!response.Contains(Protocols.Elm327.Responses.OK))
                        {
                            Diagnostics.DiagnosticLogger.Log("Could not set default frame header for PID" + System.Environment.NewLine + pid.ToString());
                            return null;
                        }

                        lastFrameHeader = Protocols.J1850.Headers.Default;
                    }
                }
            }

            // send the pid value first
            if (Send(pid))
            {
                //System.Threading.Thread.Sleep(100); // FIXME: find a better way to wait for the entire data packet than just sleeping

                // if sending was successful then try to receive
                return Receive();
            }

            // sending did not work, return false
            return null;
        }

        public override bool Send(ParameterIdentification pid)
        {
            return Send(pid.Pack());
        }

        public override bool Send(string data)
        {
            if (cableConnection.IsOpen)
            {
                Diagnostics.DiagnosticLogger.Log("Sending data " + string.Join(" ", data));
                cableConnection.Write(data);
                BytesSent += data.Length;
                return true;
            }

            return false;
        }

        public override string Receive(int timeoutMilliseconds = 300)
        {
            try
            {
                cableConnection.ReadTimeout = timeoutMilliseconds;
                string response = cableConnection.ReadTo(Protocols.Elm327.Prompt);
                response = response.Replace(Protocols.Elm327.Prompt, string.Empty); // remove the prompt character
                Diagnostics.DiagnosticLogger.Log("Received: " + response);

                // if there are more lines to read then flush the buffer,
                // there might be a new line with a prompt character or the like
                while (cableConnection.BytesToRead > 0)
                {
                    // there may be more than one line so just read everything
                    response += cableConnection.ReadExisting();
                }

                if (response.Contains(Protocols.Elm327.Responses.NoData) ||
                    response.Contains(Protocols.Elm327.Responses.Searching) ||
                    response.Contains(Protocols.Elm327.Responses.Stopped))
                {
                    Diagnostics.DiagnosticLogger.Log("Received invalid response for a PID: \"" + response + "\"");
                    return null;
                }

                BytesReceived += response.Length;
                return response;
            }
            catch (TimeoutException)
            {
                // do nothing, timeout occurred and null should be returned
                Diagnostics.DiagnosticLogger.Log("Timeout attempting read of a PID");
            }

            return null;
        }

        public override string RequestVIN()
        {
            return mode9.RequestVIN(this);
        }

        public override void ClearTroubleCodes()
        {
            Communicate(mode4);
        }

        public override List<DiagnosticTroubleCode> RequestTroubleCodes()
        {
            List<DiagnosticTroubleCode> codes = new List<DiagnosticTroubleCode>();

            // set the frame header to the default PCM for the main engine codes

            //        SendCommand(Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default), 1000);

            // turn adaptive timing off so we can get all of the codes without missing them on slow responses
            SendCommand(Protocols.Elm327.AdaptiveTimingOff, 1000);

            codes.AddRange(mode3.RequestTroubleCodes(this));
            codes.AddRange(mode7.RequestTroubleCodes(this));
            codes.AddRange(modeA.RequestTroubleCodes(this));

            // turn adaptive timing back on for pids
            SendCommand(Protocols.Elm327.AdaptiveTimingOn, 1000);

            return codes;
        }

        public override List<Tuple<DiagnosticTroubleCode, string>> RequestAllDtcStatuses()
        {
            List<Tuple<DiagnosticTroubleCode, string>> statuses = new List<Tuple<DiagnosticTroubleCode, string>>();

            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.Default);   statuses.AddRange(mode19.RequestAllDtcStatuses(this));
            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.BCM);       statuses.AddRange(mode19.RequestAllDtcStatuses(this));
            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.PCM);       statuses.AddRange(mode19.RequestAllDtcStatuses(this));
            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.TCM);       statuses.AddRange(mode19.RequestAllDtcStatuses(this));
            Protocols.Elm327.SetFrameHeader(Protocols.J1850.Headers.AirBag);    statuses.AddRange(mode19.RequestAllDtcStatuses(this));

            return statuses;
        }

    }
}
