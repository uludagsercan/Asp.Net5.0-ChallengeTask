using Business.Abstract;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LoggingHostService
{
    public class NotificationLogService : IJob
    {
        private readonly ILogService logService;

        public NotificationLogService(ILogService logService)
        {
            this.logService = logService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await logService.LogSave();
        }
    }
}
