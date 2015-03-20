using System;
using System.Threading.Tasks;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Services
{
    /// <summary>
    /// Defines a provider for logging
    /// </summary>
    public interface ILoggingProvider
    {
        /// <summary>
        /// Logs the message
        /// </summary>
        /// <param name="message">Message.</param>
        Task LogAsync(LogLevel level, string message);

        /// <summary>
        /// Logs the raw async.
        /// </summary>
        /// <returns>The raw async.</returns>
        /// <param name="level">Level.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        Task LogRawAsync(LogLevel level, object sender, string message);

        /// <summary>
        /// Gets a value indicating whether this <see cref="CF.Xamarin.Forms.Contracts.ILoggingProvider"/> should always log.
        /// </summary>
        /// <value><c>true</c> if should always log; otherwise, <c>false</c>.</value>
        bool ShouldAlwaysLog {get;}

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        Task InitializeAsync();
    }
}

