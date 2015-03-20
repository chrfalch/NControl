using System;
using System.Collections.Generic;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Providers
{
    /// <summary>
    /// View model storage provider.
    /// </summary>
    public class ViewModelStorageProvider
    {
        private readonly Dictionary<string, object> _storage = 
            new Dictionary<string, object>();

        #region IViewModelStorage implementation

        /// <summary>
        /// Sets the object for key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void SetObjectForKey<T>(string key, T value)
        {
            if (_storage.ContainsKey (key))
                _storage [key] = value;
            else
                _storage.Add (key, value);      
        }

        /// <summary>
        /// Gets the object for key.
        /// </summary>
        /// <returns>The object for key.</returns>
        /// <param name="key">Key.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T GetObjectForKey<T>(string key, T defaultValue)
        {
            if (!_storage.ContainsKey (key)) {
                if (defaultValue == null)
                    return defaultValue;

                SetObjectForKey (key, defaultValue);
            }

            return (T)Convert.ChangeType( _storage [key], typeof(T));
        }
        #endregion
    }
}

