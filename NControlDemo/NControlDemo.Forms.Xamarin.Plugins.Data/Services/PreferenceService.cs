using System;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Services;
using NControlDemo.Forms.Xamarin.Plugins.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;
using System.Threading.Tasks;
using NControlDemo.Forms.Xamarin.Plugins.Helpers;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NControlDemo.Forms.Xamarin.Plugins.Data.Services
{
    /// <summary>
    /// Preference service.
    /// </summary>
    public class PreferenceService: IPreferenceService
    {
        #region Private Members

        /// <summary>
        /// The update count.
        /// </summary>
        private int _updateCount = 0;

        /// <summary>
        /// The initialized flag.
        /// </summary>
        private bool _initializedFlag = false;

        /// <summary>
        /// The logging service.
        /// </summary>
        private ILoggingService _loggingService;

        /// <summary>
        /// The key value repository.
        /// </summary>
        private IRepository<PreferenceModel> _preferenceModelRepository;

        /// <summary>
        /// The cache.
        /// </summary>
        private readonly Dictionary<string, PreferenceModel> _cache = new Dictionary<string, PreferenceModel>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.Data.Services.PreferenceService"/> class.
        /// </summary>
        /// <param name="preferenceModelRepository">Preference model repository.</param>
        public PreferenceService(IRepository<PreferenceModel> preferenceModelRepository, ILoggingService loggingService)
        {
            _preferenceModelRepository = preferenceModelRepository;
            _loggingService = loggingService;
        }

        #region IPreferenceService implementation

        /// <summary>
        /// Persists the data to disk
        /// </summary>
        /// <returns>The async.</returns>
        public async Task PersistAsync()
        {
            foreach (var model in _cache)
                await _preferenceModelRepository.UpdateAsync(model.Value);
        }

        #endregion

        #region IService implementation

        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        public async Task InitializeAsync()
        {
            // Init repo
            await _preferenceModelRepository.InitializeAsync();

            // Fill Cache
            foreach (var model in await _preferenceModelRepository.GetItemsAsync())
                _cache.Add(model.Id, model);              
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Sets the object for key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private void SetObjectForKey<T>(Expression<Func<object>> property, T value)
        {
            EnsureInitialized();

            var key = PropertyNameHelper.GetPropertyName<PreferenceService>(property);

            _loggingService.Log(LogLevel.Verbose, this, "SetObject(key={0}, value={1})", key, value);

            var json = JsonConvert.SerializeObject(value);

            Task.Run(async () =>
                {
                    if (!_cache.ContainsKey(key))
                    {
                        // Add as new model to the repo
                        var item = new PreferenceModel{ Id = key, ValueAsJSON = json };
                        await _preferenceModelRepository.InsertAsync(item);

                        // Add to cache as well
                        _cache.Add(key, item);
                    }
                    else
                    {
                        // Update in cache
                        var item = _cache[key];
                        item.ValueAsJSON = json;
                    }

                    // Persist after a number of updates
                    if(_updateCount > 20)
                    {
                        await PersistAsync();
                        _updateCount = 0;
                    }
                    else
                    {
                        _updateCount++;
                    }

                }).Wait();        
        }

        /// <summary>
        /// Gets the object for key.
        /// </summary>
        /// <returns>The object for key.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        private T GetObjectForKey<T>(Expression<Func<object>> property, T defaultValue)
        {
            EnsureInitialized();

            var key = PropertyNameHelper.GetPropertyName<PreferenceService>(property);

            // DO NOT LOG HERE!! Logging will cause the log system to never stop since each log causes
            // a call to a preference method which in turn will log..
            //Logger.Log(LogLevel.Verbose, this, "GetObject(key={0})", key);
            if(!_cache.ContainsKey(key))
            {                    
                SetObjectForKey<T>(property, defaultValue);
                return defaultValue;
            }

            return JsonConvert.DeserializeObject<T>(_cache[key].ValueAsJSON);

        }
        #endregion

        #region Private Members

        /// <summary>
        /// Ensures that the class has been initialized and raises an exception if not
        /// </summary>
        private void EnsureInitialized()
        {
            if (_initializedFlag)            
                return;

            throw new InvalidOperationException("Preferences service needs to be initialized before it is used.");
        }
        #endregion
    }
}

