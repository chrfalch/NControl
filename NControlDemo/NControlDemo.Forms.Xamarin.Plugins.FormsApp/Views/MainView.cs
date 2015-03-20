using System;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels;
using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NGraphics;
using NControl.Plugins.Abstractions;
using NControlDemo.Forms.Xamarin.Plugins.Localization;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Views
{
    /// <summary>
    /// Main view.
    /// </summary>
    public class MainView: BaseContentsView<MainViewModel>
    {
        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            var layout = new RelativeLayout();
            layout.Children.Add(new CircularButtonControl(), () => 
                new global::Xamarin.Forms.Rectangle((layout.Width / 2) - 25, (layout.Height / 2) - 25, 50, 50));
            
            return layout;
        }
    }
}

