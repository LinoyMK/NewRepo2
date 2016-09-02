using NLog;
using System;

namespace Search.Infrastructure.Logging.NLog
{
    public interface ILogEventInfoFactory
    {
        LogEventInfo Create(LogLevel logLevel, string message, out Guid uniqueEventIdentifier, string globalEventIdentifier);
        LogEventInfo Create(LogLevel logLevel, string message, Exception ex, out Guid uniqueEventIdentifier, string globalEventIdentifier);
    }

    public class NLogEventInfoFactory : ILogEventInfoFactory
    {
        private const string UniqueEventIdentifier = "UniqueEventIdentifier";
        private const string GlobalEventIdentifier = "GlobalEventIdentifier";

        public LogEventInfo Create(LogLevel logLevel, string message, out Guid uniqueEventIdentifier, string globalEventIdentifier)
        {
            var info = new LogEventInfo(
                logLevel,
                typeof(LogManager).FullName,
                message);

            SetUniqueEventIdentifier(info, out uniqueEventIdentifier);
            SetGlobalEventIdentifier(info, globalEventIdentifier);
            return info;
        }

        public LogEventInfo Create(LogLevel logLevel, string message, Exception ex, out Guid uniqueEventIdentifier, string globalEventIdentifier)
        {
            var info = new LogEventInfo(logLevel,
                typeof(LogManager).FullName,
                null,
                message,
                null,
                ex);

            SetUniqueEventIdentifier(info, out uniqueEventIdentifier);
            SetGlobalEventIdentifier(info, globalEventIdentifier);
            return info;
        }

        private void SetUniqueEventIdentifier(LogEventInfo info, out Guid uniqueEventIdentifier)
        {
            uniqueEventIdentifier = Guid.NewGuid();
            info.Properties[UniqueEventIdentifier] = uniqueEventIdentifier;
        }

        private void SetGlobalEventIdentifier(LogEventInfo info, string globalEventIdentifier)
        {
            info.Properties[GlobalEventIdentifier] = globalEventIdentifier;
        }

    }
}
