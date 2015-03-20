using System;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels;
using Xamarin.Forms;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Views
{
    /// <summary>
    /// Menu view.
    /// </summary>
    public class MenuView: BaseContentsView<MenuViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsAppViews.MenuView"/> class.
        /// </summary>
        public MenuView()
        {
            Device.OnPlatform(
                () => Icon = ImageProvider.GetImageSource("MenuButton"),
                () => Icon = "small15x1.png");            
        }

        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            return new StackLayout();
        }
    }
}

