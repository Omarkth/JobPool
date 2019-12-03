using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JobPool
{

    public interface IJob
    {
        void SetParameters(Dictionary<string, string> parms);
        void Execute();
        int? Interval { get; }
    }

    public abstract class BasicJob : IJob
    {
        public abstract void Execute();

        public virtual void SetParameters(Dictionary<string, string> parms)
        {
            //nothing
        }

        public virtual int? Interval
        {
            get { return null; }
        }
    }

    public class JobsRegistry : FluentScheduler.Registry
    {
        public JobsRegistry()
        {
            foreach (JobConfig item in JobPoolConfig.Settings.Jobs)
            {
                System.Type type = System.Type.GetType(item.Type);
                if (typeof(JobPool.IJob).IsAssignableFrom(type))
                {
                    var parameters = new Dictionary<string, string>();
                    foreach (JobParamConfig param in item)
                    {
                        parameters.Add(param.Key, param.Value);
                    }

                    IJob j = (IJob)Activator.CreateInstance(type);
                    j.SetParameters(parameters);

                    int interval = j.Interval.HasValue ? j.Interval.Value : item.Interval;

                    this.Schedule(j.Execute).ToRunNow().AndEvery(interval).Seconds();
                }
            }
        }
    } 
}
