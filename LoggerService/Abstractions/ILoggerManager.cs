using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerService.Abstractions
{
    public interface ILoggerManager
    {
        void LogInfo(string message, params object[] args);
        void LogWarn(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogError(string message, params object[] args);
    }
}
