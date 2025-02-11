using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountOwnerServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        private IRepositoryWrapper _repositoryWrapper;

        public WeatherForecastController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpGet]
        [Route("getlogs")]
        public IEnumerable<string> Get()
        {
          
            _logger.LogInfo("loginfo via weatherforcast controller get method");
            _logger.LogDebug("logdebug via weatherforcast controller get method");
            _logger.LogWarning("logwarning via weatherforcast controller get method");
            _logger.LogError("logerror via weatherforcast controller get method");

            return new string[] { "nlogs are filed to->", ".\\BankAccountOwnerServer\\internal_logs" };

        }

        [HttpGet]
        [Route("testing")]
        public IEnumerable<string> Testing()
        {
            //testing repositorywrapper
            var owners = _repositoryWrapper.Owner.FindAll();
            var domesticAccounts = _repositoryWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));

            return new string[] { "owners", "domesticAccounts" };
           
        }


    }
}
