﻿using NLog;//logManager is a class in NLog namespace
using Contracts;//add project reference to Contracts from logging service project

namespace LoggingService
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger => LogManager.GetCurrentClassLogger();
        public void LogDebug(string message) => logger.Debug(message);
        public void LogError(string message) => logger.Error(message);
        public void LogInfo(string message) => logger.Info(message);
        public void LogWarning(string message) => logger.Warn(message);


    }
}
