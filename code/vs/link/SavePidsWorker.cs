using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;

using OBD2;
using OBD2.Cables;

namespace link
{
    public class SavePidsWorker : Worker
    {

        public SavePidsWorker(BlockingCollection<Tuple<double, ParameterIdentification>> decodedValues)
        {
            this.decodedValues = decodedValues;
        }


        protected override void DoWork()
        {
            try
            {
                CreateLogFilePath();

                while (!StopWork)
                {
                    Tuple<double, ParameterIdentification> decodedPid;
                    if (decodedValues.TryTake(out decodedPid, 100))
                    {
                        // get the Item1 and Item2 properties into variable names that make more sense
                        double pidValue = decodedPid.Item1;
                        ParameterIdentification pid = decodedPid.Item2;

                        string pidFilePath = BuildPidFilePath(pid);
                        StringBuilder text = new StringBuilder(50);

                        if (!System.IO.File.Exists(pidFilePath))
                        {
                            // no need to create the file here, most of the file.open() functions do this for us
                            //System.IO.File.Create(pidFilePath);

                            // if the file does not exist then it needs headers
                            text.Append("Timestamp,");
                            text.Append(pid.Name);
                            text.Append(System.Environment.NewLine);
                        }

                        text.Append(pid.Timestamp.ToString());
                        text.Append(",");
                        text.Append(pidValue);
                        text.Append(System.Environment.NewLine);

                        // AppendAllText() is not very efficient in a loop, it will open the file, append, then close it,
                        // a more efficient way to do this is to store the stream writers and look them up when needed,
                        // but for now this will suffice to get an initial version working
                        File.AppendAllText(pidFilePath, text.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Diagnostics.DiagnosticLogger.Log("SavePidsWorker encountered an error", e);
            }
        }

        private string BuildPidFilePath(ParameterIdentification pid)
        {
            return System.IO.Path.Combine(finalDirectory, pid.Name + ".csv");
        }

        private void CreateLogFilePath()
        {
            string baseDirectory = Properties.Settings.Default.LogToDirectory;
            string date = DateTime.Today.ToShortDateString();
            string baseDateDirectory = System.IO.Path.Combine(baseDirectory, date);
            
            // if the directory exists find the highest number and increase by one,
            // otherwise just start the counting at one and create the directory
            if (System.IO.Directory.Exists(baseDateDirectory))
            {
                int maxDirNum = 0;
                List<string> existingDirectories = System.IO.Directory.EnumerateDirectories(baseDateDirectory).ToList();

                foreach (string existingDirectory in existingDirectories)
                {
                    int dirNum = 0;
                    string existingDirectoryName = Path.GetFileName(existingDirectory);
                    if (int.TryParse(existingDirectoryName, out dirNum))
                    {
                        if (dirNum > maxDirNum)
                        {
                            maxDirNum = dirNum;
                        }
                    }
                }

                finalDirectory = System.IO.Path.Combine(baseDateDirectory, (maxDirNum + 1).ToString());
            }
            else
            {
                // start the directory counting at one
                finalDirectory = System.IO.Path.Combine(baseDateDirectory, "1");
            }

            // create the final directory structure
            System.IO.Directory.CreateDirectory(finalDirectory);
        }

        private string finalDirectory = string.Empty;
        private BlockingCollection<Tuple<double, ParameterIdentification>> decodedValues = null;

    }
}
