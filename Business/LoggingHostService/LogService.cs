using Business.Abstract;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.LoggingHostService
{
    public class LogService : BackgroundService
    {
        private readonly ILogService _logService;

        public LogService(ILogService logService)
        {
            _logService = logService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Thread.Sleep(TimeSpan.FromMinutes(0.5));
                await _logService.LogSave();
            }
        }
    }
}
