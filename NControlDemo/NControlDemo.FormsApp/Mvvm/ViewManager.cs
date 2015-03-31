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
using NControlDemo.FormsApp.ViewModels;
using Xamarin.Forms;
using NControlDemo.FormsApp.IoC;

namespace NControlDemo.FormsApp.Mvvm
{
    /// <summary>
    /// Implements helper methods for implementing Mvvm
    /// </summary>
    public static class ViewManager
    {
        /// <summary>
        /// Viewmodel/View mapping
        /// </summary>
        private static Dictionary<Type, Type> ViewModels = new Dictionary<Type, Type>();

        /// <summary>
        /// Registers the view.
        /// </summary>
        /// <typeparam name="TViewModelType">The 1st type parameter.</typeparam>
        /// <typeparam name="TViewType">The 2nd type parameter.</typeparam>
        public static void RegisterView<TViewModelType, TViewType>()
            where TViewModelType : BaseViewModel
            where TViewType : Page
        {
            ViewModels.Add (typeof(TViewModelType), typeof(TViewType));
        }

        /// <summary>
        /// Gets the view from view model.
        /// </summary>
        /// <returns>The view from view model.</returns>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Page GetViewFromViewModel<TViewModel>() where TViewModel : BaseViewModel
        {
            return GetViewFromViewModel(typeof(TViewModel));
        }

        /// <summary>
        /// Gets the view from view model. Uses naming conventions to lookup the view:
        /// The name of the view model minus the word model. Also namespace viewmodel => view
        /// </summary>
        /// <returns>The view from view model.</returns>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        public static Page GetViewFromViewModel(Type viewModelType)
        {
            return Container.Resolve (ViewModels [viewModelType]) as Page;
        }
    }
}

