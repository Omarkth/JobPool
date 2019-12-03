using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPool
{
    public class Pool
    {
        private static bool _started = false;

        public static void Start()
        {
            if (!_started)
            {
                _started = true;
                FluentScheduler.TaskManager.Initialize(new JobPool.JobsRegistry());
            }
        }

        public static void Stop()
        {
            if (_started)
            {
                _started = false;
                FluentScheduler.TaskManager.Stop();
            }
        }

    }
}
