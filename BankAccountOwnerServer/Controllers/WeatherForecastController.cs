using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOwnerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            _logger.LogInfo("loginfo via weatherforcast controller get method");
            _logger.LogDebug("logdebug via weatherforcast controller get method");
            _logger.LogWarning("logwarning via weatherforcast controller get method");
            _logger.LogError("logerror via weatherforcast controller get method");

            return new string[] { "nlogs are filed to->", "C:\\Users\\dasmac\\source\\repos\\BankAccountOwnerServer\\BankAccountOwnerServer\\internal_logs" };
        }

    }
}
