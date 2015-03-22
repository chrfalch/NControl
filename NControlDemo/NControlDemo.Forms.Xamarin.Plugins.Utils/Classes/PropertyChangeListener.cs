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
using System.ComponentModel;
using System.Linq.Expressions;
using NControlDemo.Helpers;

namespace NControlDemo.Classes
{
    /// <summary>
    /// Implements a simple property changed listener
    /// </summary>
    public class PropertyChangeListener: IDisposable
    {
        #region Private Members

        /// <summary>
        /// The notify changed object.
        /// </summary>
        private INotifyPropertyChanged _notifyChangedObject;

        /// <summary>
        /// The callback.
        /// </summary>
        private Action _callback;

        /// <summary>
        /// The name of the property.
        /// </summary>
        private string _propertyName;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/> class.
        /// </summary>
        /// <param name="owner">Owner.</param>
        /// <param name="callback">Callback.</param>
        public void Listen<TModel>(Expression<Func<TModel, object>> property, object owner, Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException("callback");

            if (owner == null)
                throw new ArgumentNullException("owner");

            _notifyChangedObject = owner as INotifyPropertyChanged;
            if (_notifyChangedObject == null)
                throw new ArgumentNullException("owner as IPropertyNotifyChanged");

            _propertyName = PropertyNameHelper.GetPropertyName<TModel>(property);
            if (string.IsNullOrEmpty(_propertyName))
                throw new ArgumentException("property");

            _callback = callback;

            // Hook up event
            _notifyChangedObject.PropertyChanged += HandlePropertyChangedEventHandler;

            // Start by updating:
            _callback();
        }

        #region IDisposable implementation

        /// <summary>
        /// Releases all resource used by the <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/> object.
        /// </summary>
        /// <remarks>Call <see cref="Dispose"/> when you are finished using the
        /// <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/>. The <see cref="Dispose"/> method leaves the
        /// <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/> in an unusable state. After calling
        /// <see cref="Dispose"/>, you must release all references to the
        /// <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/> so the garbage collector can reclaim the
        /// memory that the <see cref="CF.Xamarin.Forms.Classes.PropertyChangeListener"/> was occupying.</remarks>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The bulk of the clean-up code is implemented in Dispose(bool)
        /// </summary>
        /// <param name="disposing">If set to <c>true</c> disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing) 
            {
                // free managed resources
                _notifyChangedObject.PropertyChanged -= HandlePropertyChangedEventHandler;
                _notifyChangedObject = null;
                _callback = null;
                _propertyName = null;
            }

            // free native resources if there are any.
            // We dont have any
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the property changed event handler.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandlePropertyChangedEventHandler (object sender, PropertyChangedEventArgs e)
        {
            if(_propertyName == e.PropertyName)
                _callback();
        }
        #endregion
    }
}

