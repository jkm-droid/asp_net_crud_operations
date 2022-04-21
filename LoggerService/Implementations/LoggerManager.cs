using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerService.Abstractions;
using Microsoft.Extensions.Logging;
using NLog;

namespace LoggerService.Implementations
{
    public class LoggerManager : ILoggerManager
    {
        private readonly ILogger<LoggerManager> _logger;

        public LoggerManager(ILogger<LoggerManager> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        public void LogWarn(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        public void LogDebug(string message, params object[] args)
        {
           _logger.LogDebug(message, args);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }
    }
}
