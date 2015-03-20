using System;
using System.Threading.Tasks;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Services;

namespace NControlDemo.Forms.Xamarin.Plugins.Data.Providers
{
    public class DiagnosticsLoggingProvider: ILoggingProvider
    {
        #region ILoggingProvider implementation

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Log the specified formatString and args.
        /// </summary>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Arguments.</param>
        public Task LogAsync(LogLevel level, string message)
        {
            System.Diagnostics.Debug.WriteLine("{0}: {1}", level, message);
            return Task.FromResult(true);
        }

        /// <summary>
        /// Logs the raw async.
        /// </summary>
        /// <returns>The raw async.</returns>
        /// <param name="level">Level.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        public Task LogRawAsync(LogLevel level, object sender, string message)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="CF.Xamarin.Forms.Providers.DiagnosticsLoggingProvider"/>
        /// should always log.
        /// </summary>
        /// <value><c>true</c> if should always log; otherwise, <c>false</c>.</value>
        public bool ShouldAlwaysLog { get {return false;}}

        #endregion
    }
}

