using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace link
{
    public class Worker
    {
        /// <summary>
        /// Instantiates a new instance of Worker.
        /// </summary>
        /// <param name="threadName"> The name to assign to the thread. </param>
        public Worker()
        {
            // do nothing for now
        }


        ~Worker()
        {
            if (IsAlive)
            {
                this.Stop();
            }
        }


        /// <summary>
        /// Starts the worker thread.
        /// </summary>
        public void Start()
        {
            stopWork = false;
            workerThread = new Thread(DoWork);
            workerThread.Start();
        }


        /// <summary>
        /// Stops the worker thread.
        /// </summary>
        public void Stop()
        {
            lock (locker)
            {
                stopWork = true;
            }
        }


        /// <summary>
        /// Blocks the calling thread until the worker has finished.
        /// </summary>
        public void Join()
        {
            if (workerThread != null)
            {
                workerThread.Join();
            }
        }


        /// <summary>
        /// Executes the work to be done.
        /// </summary>
        virtual protected void DoWork()
        {
            // do nothing for now
        }


        /// <summary>
        /// Returns the state of the thread.
        /// </summary>
        public bool IsAlive
        {
            get
            {
                return (workerThread != null) ? workerThread.IsAlive : false;
            }
        }


        /// <summary>
        /// Returns the requested state of executing work. This does not reflect the current state of executing work.
        /// </summary>
        public bool StopWork
        {
            get
            {
                lock (locker)
                {
                    return stopWork;
                }
            }
        }


        private Thread workerThread = null;
        private object locker = false;
        private bool stopWork = false;
    }
}
