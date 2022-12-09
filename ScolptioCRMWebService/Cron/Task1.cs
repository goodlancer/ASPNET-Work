using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ScolptioCRMWebApi.Cron
{
        public class Task1 : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var task = Task.Run(() => logfile(DateTime.Now)); ;
                return task;
            }
            public void logfile(DateTime time)
            {
                string path = "C:\\log\\sample.txt";
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(time);
                    writer.Close();
                }
            }
        }
}
