using System;
using NControlDemo.Forms.Xamarin.Plugins.Localization;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels
{
    /// <summary>
    /// Menu view model.
    /// </summary>
    public class MenuViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsAppViewModels.MenuViewModel"/> class.
        /// </summary>
        public MenuViewModel()
        {
        }

        #region Properties

        /// <summary>
        /// Returns the view title
        /// </summary>
        /// <value>The view title.</value>
        public override string Title
        {
            get
            {
                return Strings.TitleMenu;
            }
        }

        #endregion
    }
}

