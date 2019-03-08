using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzStudy
{
    public class JobInstance : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            IDictionary<string, object> jobs = context.JobDetail.JobDataMap;
            if (jobs != null)
            {
                foreach (var item in jobs.Values)
                {
                    try
                    {
                        Console.WriteLine(item.ToString());
                        JobBase jobObj = item as JobBase;
                        jobObj.Run();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
