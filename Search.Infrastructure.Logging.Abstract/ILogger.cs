using System;

namespace Search.Infrastructure.Logging.Abstract
{
    /// <summary>
    /// Represents the contract to comply as logger of the system
    /// <see cref="http://www.ibm.com/developerworks/java/library/j-logging/">Suggestions for logging behaviour</see>
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the message for Debug level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Debug(string message, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with DEBUG level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsDebugEnabled();

        /// <summary>
        /// Logs the message for Application Errors level 
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Error(string message, string identifier = "");

        /// <summary>
        /// Logs a exception details, including inner exceptions, for Application Errors level 
        /// </summary>
        /// <param name="ex">exception to be analyzed and logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Error(Exception ex, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with ERROR level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsErrorEnabled();

        /// <summary>
        ///  Logs the message for Fatal Errors level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Fatal(string message, string identifier = "");
        Guid Fatal(Exception ex, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with FATAL level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsFatalEnabled();

        /// <summary>
        ///  Logs the message for Information level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Info(string message, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with INFO level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsInfoEnabled();

        /// <summary>
        ///  Logs the message for Tracing level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Trace(string message, string identifier = "");

        /// <summary>
        ///  Logs the message for Tracing level and for a specific logger
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="logType">the logger that must perform the logging</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Trace(string message, LogType logType, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with TRACE level
        /// </summary>
        /// <returns>Is enabled or not</returns>
        bool IsTraceEnabled();

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with TRACE level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsTraceEnabled(LogType logType);

        /// <summary>
        ///  Logs the message for Warning level
        /// </summary>
        /// <param name="message">message to be logged</param>
        /// <param name="identifier">the identifier of the event</param>
        Guid Warn(string message, string identifier = "");

        /// <summary>
        /// This method informs to the application if the logger is currently configured to render traces with WARN level
        /// </summary>
        /// <param name="logType">The logger that must perform the logging</param>
        /// <returns>Is enabled or not</returns>
        bool IsWarnEnabled();
    }

    /// <summary>
    /// This enum is used to avoid messing arround with logger names in the code. 
    /// You only have to select one of the possible values.
    /// </summary>
    public enum LogType
    {
        Regular
    }
}
