using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Services;
using NControlDemo.Forms.Xamarin.Plugins.Data.Providers;

namespace NControlDemo.Forms.Xamarin.Plugins.Data.Services
{
    /// <summary>
    /// Implementation of the Logging service.
    /// </summary>
    public class LoggingService: ILoggingService
    {
        #region Private Members

        /// <summary>
        /// The providers.
        /// </summary>
        private readonly List<ILoggingProvider> _providers = new List<ILoggingProvider>();

        /// <summary>
        /// The ignore count.
        /// </summary>
        private int _ignoreCount = 0;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CF.Xamarin.Forms.Services.LoggingService"/> class.
        /// </summary>
        public LoggingService()
        {
            LogLevel = LogLevel.All;
        }

        #region ILoggingService implementation

        /// <summary>
        /// Initialization code
        /// </summary>
        /// <returns>The async.</returns>
        public Task InitializeAsync()
        {
            #if DEBUG
            return AddProviderAsync(new DiagnosticsLoggingProvider());
            #else
            return Task.FromResult(true);
            #endif
        }

        /// <summary>
        /// Adds a new provider to the logging service
        /// </summary>
        /// <param name="provider">Provider.</param>
        public async Task AddProviderAsync(ILoggingProvider provider)
        {
            await provider.InitializeAsync();
            _providers.Add(provider);
        }
                   
        /// <summary>
        /// Log the specified level, formatString and args.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="sender">Sender.</param>
        /// <param name="message">Message.</param>
        public void Log(LogLevel level, object sender, string message)
        {
            if (_ignoreCount > 0)
                return;

            var formattedMessage = string.Format("{0}{1} [{2}]:{3}", (sender != null ? sender.GetType().Name + " - " : string.Empty),
                DateTime.Now.ToString("t"), level, message);

            Task.Run(async () => {
            
                var providers = new List<ILoggingProvider>(_providers);

                // Loop through providers
                foreach (var p in providers)
                {
                    if(level > LogLevel && !p.ShouldAlwaysLog)
                        continue;
                    
                    await p.LogAsync(level, formattedMessage);
                    await p.LogRawAsync(level, sender, message);                
                }
            });
        }

        /// <summary>
        /// Log the specified level, formatString and args.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="sender">Sender.</param>
        public void Log(LogLevel level, object sender, string formatString, params object[] args)
        {
            Log(level, sender, string.Format(formatString, args));
        }

        /// <summary>
        /// Log the specified formatString and args.
        /// </summary>
        /// <param name="formatString">Format string.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="level">Level.</param>
        /// <param name="message">Message.</param>
        public void Log(LogLevel level, string message)
        {
            Log(level, this, message);
        }

        /// <summary>
        /// Filters the level for logging
        /// </summary>
        /// <value>The log level.</value>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// Begins ignoring updates (use when sending long strings of data or other info that you don't want to be logged)
        /// </summary>
        public void BeginIgnore()
        {
            Interlocked.Increment(ref _ignoreCount);
        }

        /// <summary>
        /// Ends the ignore cycle
        /// </summary>
        public void EndIgnore()
        {
            Interlocked.Decrement(ref _ignoreCount);
        }
        #endregion
    }
}

