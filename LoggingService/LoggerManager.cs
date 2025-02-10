using Contracts;
using NLog;

namespace LoggingService
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger logger => LogManager.GetCurrentClassLogger();

    }
}
