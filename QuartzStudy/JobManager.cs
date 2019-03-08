using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzStudy
{
    public class JobManager
    {
        IScheduler scheduler;

        public async Task AddJob<T>(int Second) where T : JobBase
        {
            scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            JobBase jbobj = Activator.CreateInstance<T>();
            IDictionary<string, object> jbData = new Dictionary<string, object>();
            jbData.Add("name", jbobj);

            IJobDetail job1 = JobBuilder.Create<JobInstance>()
                .SetJobData(new JobDataMap(jbData)).Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Second)
                .RepeatForever()).Build();

            await scheduler.ScheduleJob(job1, trigger1);
        }

        public async Task AddJob<T>(string rule) where T : JobBase
        {
            scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            JobBase jbInstance = Activator.CreateInstance<T>();
            IDictionary<string, object> jbData = new Dictionary<string, object>();
            jbData.Add("name", jbInstance);

            IJobDetail job1 = JobBuilder.Create<JobInstance>()
                .SetJobData(new JobDataMap(jbData)).Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule(rule).Build();

            await scheduler.ScheduleJob(job1, trigger1);
        }
    }
}
