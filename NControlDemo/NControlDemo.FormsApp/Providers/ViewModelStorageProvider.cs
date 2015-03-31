/************************************************************************
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2025 - Christian Falch
 * 
 * Permission is hereby granted, free of charge, to any person obtaining 
 * a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject 
 * to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************/

using System;
using System.Collections.Generic;

namespace NControlDemo.FormsApp.Providers
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

