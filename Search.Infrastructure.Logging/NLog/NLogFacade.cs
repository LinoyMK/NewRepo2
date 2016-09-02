using NLog;
using NLog.Config;
using Search.Infrastructure.Logging.Abstract;
using Search.Infrastructure.Logging.NLog.Targets;
using System;

namespace Search.Infrastructure.Logging.NLog
{
    /// <summary>
    /// This facade isolates the application from the 
    /// </summary>
    public class NLogFacade : Abstract.ILogger
    {
        private readonly Logger _logger;
        private readonly ILogEventInfoFactory _logEventInfoFactory;

        public NLogFacade(ILogEventInfoFactory logEventInfoFactory)
        {
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("RedisList", typeof(RedisListTarget));
            ConfigurationItemFactory.Default.Targets.RegisterDefinition("RedisChannel", typeof(RedisChannelTarget));
            _logEventInfoFactory = logEventInfoFactory;
            _logger = LogManager.GetCurrentClassLogger();
        }

        #region "Debug related methods"

        /// <summary>
        /// Logs the message for Debug level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">identifier of the message to be logged</param>
        public Guid Debug(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Debug, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with DEBUG level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>        
        public bool IsDebugEnabled()
        {
            return GetLogger().IsDebugEnabled;
        }

        #endregion

        #region "Error related methods"

        /// <summary>
        /// Logs the message for Application Errors level 
        /// </summary>
        /// <param name="message">message to be logged</param>
        public Guid Error(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Error, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// Logs a exception details, including inner exceptions, for Application Errors level 
        /// </summary>
        /// <param name="ex">exception to be analyzed and logged</param>
        public Guid Error(Exception ex, string identifier = "")
        {
            string message = ex.Message;
            Exception tempException = ex.InnerException;

            while (tempException != null)
            {
                message += Environment.NewLine + tempException.Message;
                tempException = tempException.InnerException;
            }

            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Error, message, ex, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with ERROR level
        /// </summary>        
        /// <returns>Is enabled or not</returns>
        public bool IsErrorEnabled()
        {
            return GetLogger().IsErrorEnabled;
        }

        #endregion

        #region "Fatal related methods"

        public Guid Fatal(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Fatal, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        public bool IsFatalEnabled()
        {
            return GetLogger().IsFatalEnabled;
        }

        public Guid Fatal(Exception ex, string identifier = "")
        {
            string message = ex.Message;
            Exception tempException = ex.InnerException;

            while (tempException != null)
            {
                message += Environment.NewLine + tempException.Message;
                tempException = tempException.InnerException;
            }

            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Fatal, message, ex, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        #endregion

        #region "Info related methods"

        /// <summary>
        ///  Logs the message for Information level
        /// </summary>
        /// <param name="message">message to be logged</param>
        public Guid Info(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Info, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with INFO level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        public bool IsInfoEnabled()
        {
            return GetLogger().IsInfoEnabled;
        }

        #endregion

        #region "Trace related methods"

        /// <summary>
        ///  Logs the message for Tracing level
        /// </summary>
        /// <param name="message">message to be logged</param>
        public Guid Trace(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Trace, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        ///  Logs the message for Tracing level and for a specific logger
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="logType">the logger that must perform the logging</param>
        public Guid Trace(string message, LogType logType, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Trace, message, out uniqueEventIdentifier, identifier);

            GetLogger(logType).Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with TRACE level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        public bool IsTraceEnabled()
        {
            return GetLogger().IsTraceEnabled;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with TRACE level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        public bool IsTraceEnabled(LogType logType)
        {
            return GetLogger(logType).IsTraceEnabled;
        }

        #endregion

        #region "Warn related methods"

        /// <summary>
        ///  Logs the message for Warning level
        /// </summary>
        /// <param name="message">message to be logged</param>
        public Guid Warn(string message, string identifier = "")
        {
            Guid uniqueEventIdentifier;
            var info = _logEventInfoFactory.Create(LogLevel.Warn, message, out uniqueEventIdentifier, identifier);

            GetLogger().Log(info);
            return uniqueEventIdentifier;
        }

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with WARN level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        public bool IsWarnEnabled()
        {
            return GetLogger().IsWarnEnabled;
        }

        #endregion

        #region "Private methods"

        /// <summary>
        /// This method returns the general logger instance, and instanciate if the first time it's requested.
        /// </summary>
        /// <returns></returns>
        private Logger GetLogger()
        {
            return _logger;
        }

        /// <summary>
        /// This method makes the selection of the proper instance of the logger. It also makes the initialization of the instance
        /// if it is not done yet
        /// </summary>
        /// <param name="logType">The type of logger required (regular, input, output)</param>
        /// <returns>The logger instance</returns>
        private Logger GetLogger(LogType logType)
        {
            switch (logType)
            {
                case LogType.Regular:
                    return GetLogger();
                default:
                    throw new NotSupportedException("The logType argument has a not supported value");
            }
        }

        #endregion
    }
}

