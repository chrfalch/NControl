using System;
using NControlDemo.Forms.Xamarin.Plugins.Localization;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels
{
    /// <summary>
    /// Main view model.
    /// </summary>
    public class MainViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsAppViewModels.MainViewModel"/> class.
        /// </summary>
        public MainViewModel()
        {
        }

        /// <summary>
        /// Returns the view title
        /// </summary>
        /// <value>The view title.</value>
        public override string Title
        {
            get
            {
                return Strings.AppName;
            }
        }
    }
}

