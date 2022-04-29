using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : Controller
    {

        private readonly ILogService _logService;

        public LogsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet("systemlogs")]
        public IActionResult SystemLogs(string date, string level)
        {
            var result =_logService.GetAllByDateAndLevel(date, level);
            return Ok(result);
        }
    }
}
