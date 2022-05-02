using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LoggingHostService
{
    public class QuartServiceUtility
    {
        public static void StartJob<T> (IScheduler scheduler, TimeSpan timeSpan) where T : IJob
        {
            var jobName = typeof(T).FullName;
            var job = JobBuilder.Create<T> ().WithIdentity(jobName).Build();
            var trigger = TriggerBuilder.Create().WithIdentity($"{jobName}.trigger").StartNow().WithSimpleSchedule(schedulerBuilder => 
            schedulerBuilder.WithInterval(timeSpan).RepeatForever()).Build();
            scheduler.ScheduleJob(job, trigger);

        }
    }
}
