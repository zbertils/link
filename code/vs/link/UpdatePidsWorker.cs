using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

using OBD2;
using OBD2.Cables;

namespace link
{
    public class UpdatePidsWorker : Worker
    {

        public UpdatePidsWorker(Cable obdCable, List<OBD2.ParameterIdentification> pids, BlockingCollection<Tuple<double, ParameterIdentification>> decodedValues)
        {
            this.cable = obdCable;
            this.pids = pids;
            this.decodedValues = decodedValues;
        }


        protected override void DoWork()
        {
            try
            {
                if (cable != null && cable.IsOpen && pids != null)
                {
                    while (!StopWork)
                    {
                        // only iterate over the pids that are being logged
                        foreach (ParameterIdentification pid in pids.Where(p => p.LogThisPID))
                        {
                            string data = cable.Communicate(pid);
                            if (!string.IsNullOrEmpty(data))
                            {
                                double value = pid.Unpack(data);
                                pid.LastDecodedValue = value;

                                // if trying to log to file and the concurrent queue is available then do so
                                if (Properties.Settings.Default.LogToFile)
                                {
                                    if (decodedValues != null)
                                    {
                                        decodedValues.Add(new Tuple<double, ParameterIdentification>(value, pid));
                                    }
                                    else if (decodedValues == null)
                                    {
                                        Diagnostics.DiagnosticLogger.Log("Trying to save a decoded value onto concurrent queue that is null!");
                                    }
                                }
                            }

                            if (StopWork)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Stop();
                }
            }
            catch (Exception e)
            {
                Diagnostics.DiagnosticLogger.Log("UpdatePidsWorker encountered an error", e);
            }
        }

        /// <summary>
        /// Sets the cable used by the worker thread.
        /// </summary>
        /// <param name="cable"> The cable to use. </param>
        /// <remarks>
        /// This function stops the worker thread if it is running and does not restart it.
        /// </remarks>
        public void SetCable(Cable cable)
        {
            // stop and join the worker thread
            Stop();
            Join();
            this.cable = cable;
        }

        /// <summary>
        /// Sets the PID list used by the worker thread.
        /// </summary>
        /// <param name="pids"> The PID list to use. </param>
        /// <remarks>
        /// This function stops the worker thread if it is running and does not restart it.
        /// </remarks>
        public void SetPids(List<ParameterIdentification> pids)
        {
            Stop();
            Join();
            this.pids = pids;
        }

        private Cable cable = null;
        private List<ParameterIdentification> pids = null;
        private BlockingCollection<Tuple<double, ParameterIdentification>> decodedValues = null;

    }
}
