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

