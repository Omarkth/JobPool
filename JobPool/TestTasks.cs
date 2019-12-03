using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPool
{
    internal class MyTasks
    {
        public static int ID = 0;
        
        public static int GetID()
        {
            int _id = -1;
            lock ("MyLock")
            {
                ID += 1;
                _id = ID;
            }
            return _id;
        }

        public class Task1 : IJob
        {
            public void Execute()
            {
                int _id = GetID();
                Console.WriteLine("Task 1 Started: " + _id);
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Task 1 Ended: " + _id);
            }

            private int? _interval;
            public void SetParameters(Dictionary<string, string> parms)
            {
                int value;
                if (parms.ContainsKey("interval") && int.TryParse(parms["interval"], out value))
                {
                    _interval = value;
                }
            }

            public int? Interval
            {
                get { return _interval; }
            }
        }

        public class Task2 : BasicJob
        {
            public override void Execute()
            {
                int _id = GetID();
                Console.WriteLine("Task2: " + _id);
            }
             
        }
    }
    
}
