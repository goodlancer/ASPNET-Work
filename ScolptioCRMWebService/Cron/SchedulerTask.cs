using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Cron
{
    public class SchedulerTask
    {
        private static readonly string ScheduleCronExpressionDaily = "0 0 0 * * ?";
        private static readonly string ScheduleCronExpressionWeekly = "0 0 12 */7 * ?";
        private static readonly string ScheduleCronExpressionMonthly = "0 0 12 1 * ?";
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }
                //First Task
                var job1 = JobBuilder.Create<Task1>().WithIdentity("ExecuteTaskServiceCallJob1", "group1").Build();
                var trigger1 = TriggerBuilder.Create().WithIdentity("ExecuteTaskServiceCallTrigger1", "group1").WithCronSchedule(ScheduleCronExpressionDaily).Build();
                //  Second Task
                var job2 = JobBuilder.Create<Task2>().WithIdentity("ExecuteTaskServiceCallJob2", "group2").Build();
                var trigger2 = TriggerBuilder.Create().WithIdentity("ExecuteTaskServiceCallTrigger2", "group2").WithCronSchedule(ScheduleCronExpressionWeekly).Build();
                //  Third Task
                var job3 = JobBuilder.Create<Task3>().WithIdentity("ExecuteTaskServiceCallJob3", "group3").Build();
                var trigger3 = TriggerBuilder.Create().WithIdentity("ExecuteTaskServiceCallTrigger3", "group3").WithCronSchedule(ScheduleCronExpressionMonthly).Build();
                
                await scheduler.ScheduleJob(job1, trigger1);
                await scheduler.ScheduleJob(job2, trigger2);
                await scheduler.ScheduleJob(job3, trigger3);
            }
            catch (Exception ex) { }
        }
    }
}
