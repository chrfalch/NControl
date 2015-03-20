using System;
using System.Threading.Tasks;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Services
{
    /// <summary>
    /// Defines log levels
    /// </summary>
    public enum LogLevel
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Verbose = 3,
        All = 4
    }

    /// <summary>
    /// I logging service.
    /// </summary>
    public interface ILoggingService: IService
    {

        /// <summary>
        /// Adds a new provider to the logging service
        /// </summary>
        /// <param name="provider">Provider.</param>
        Task AddProviderAsync(ILoggingProvider provider);

        /// <summary>
        /// Log the specified level and message.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="message">Message.</param>
        void Log(LogLevel level, object sender, string message);

        /// <summary>
        /// Log the specified level, formatString and args.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Arguments.</param>
        void Log(LogLevel level, object sender, string formatString, params object[] args);

        /// <summary>
        /// Begins ignoring updates (use when sending long strings of data or other info that you don't want to be logged)
        /// </summary>
        void BeginIgnore();

        /// <summary>
        /// Ends the ignore cycle
        /// </summary>
        void EndIgnore();

        /// <summary>
        /// Filters the level for logging
        /// </summary>
        /// <value>The log level.</value>
        LogLevel LogLevel { get; set; }
    }
}

