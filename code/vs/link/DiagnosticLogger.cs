using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace Diagnostics
{
    public static class DiagnosticLogger
    {
        private static Thread loggingThread = new Thread(LoggingWorker);
        private static bool isAlive = false;
        private static BlockingCollection<string> messageQueue = new BlockingCollection<string>();
        private const int separationCharacterCount = 79; // the common margin column
        private const int messageQueueTimeout = 1000; // in milliseconds
        private static string loggerFileName = System.IO.Path.GetTempPath() + "link.log";


        /// <summary>
        /// Starts the logging thread for the application.
        /// </summary>
        /// <param name="fileName"> The optional file name to give to the log file. </param>
        public static void StartLogging(string fileName = null)
        {
            isAlive = true;
            loggerFileName = fileName ?? System.IO.Path.GetTempPath() + "link.log";
            loggingThread.Start();
        }


        /// <summary>
        /// Stops the logging thread.
        /// </summary>
        public static void StopLogging()
        {
            isAlive = false;
        }


        /// <summary>
        /// Logs the given message with the optional exception to also log.
        /// </summary>
        /// <param name="message"> The message to log. </param>
        /// <param name="exception"> The optional exception accompanying the log message. </param>
        public static void Log(string message, Exception exception = null)
        {
            // add the starting message characters
            string logMessage = "";
            for (int i = 0; i < separationCharacterCount; i++)
            {
                logMessage += "-";
            }
            logMessage += Environment.NewLine;

            // add the time and message to be logged
            DateTime messageTime = DateTime.Now;
            logMessage += messageTime.ToLongDateString() + " - " + messageTime.ToLongTimeString() + " ";
            logMessage += Environment.NewLine + message;

            // if the exception is not null then log the details about that too
            if (exception != null)
            {
                logMessage += Environment.NewLine + Environment.NewLine +
                    "Exception Message: " + exception.Message;

                logMessage += Environment.NewLine + Environment.NewLine +
                    "Exception Type: " + exception.GetType().ToString();

                logMessage += Environment.NewLine + Environment.NewLine +
                    "Exception Stack Trace: " + Environment.NewLine + exception.StackTrace;
            }

            // add the ending message characters
            logMessage += Environment.NewLine;
            for (int i = 0; i < separationCharacterCount; i++)
            {
                logMessage += "-";
            }
            logMessage += Environment.NewLine + Environment.NewLine;

            // put the new message on the queue
            // and tell the worker thread to log
            messageQueue.Add(logMessage);
        }


        /// <summary>
        /// The logging worker
        /// </summary>
        private static void LoggingWorker()
        {
            // open the logging file
            System.IO.StreamWriter logFile = new System.IO.StreamWriter(loggerFileName, true);

            // while logging is alive and should continue
            while (isAlive)
            {
                string message = string.Empty;

                // make sure the queue is not empty
                if (messageQueue.TryTake(out message, messageQueueTimeout))
                {
                    // if the message could be dequeued then log it
                    logFile.Write(message);
                    logFile.Flush();

#if DEBUG
                    // if debug mode, write to the console as well
                    System.Diagnostics.Debug.Write(message);
#endif
                }
            }

            logFile.Close();
        }

    }
}
