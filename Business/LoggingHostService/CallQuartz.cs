using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LoggingHostService
{
    public class CallQuartz
    {
        public static void UseQuartz(IServiceCollection serviceCollection, params Type[] jobs)
        {
            serviceCollection.AddSingleton<IJobFactory, JobFactory>();
            serviceCollection.Add(jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)));
            serviceCollection.AddSingleton(provider =>
            {
                var sFactory = new StdSchedulerFactory();
                var scheduler = sFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });
        }
    }
}
