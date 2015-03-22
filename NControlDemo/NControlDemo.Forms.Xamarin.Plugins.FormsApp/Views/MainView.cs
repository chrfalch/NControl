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
        /// The bottom bar.
        /// </summary>
        private RelativeLayout _bottomBar;

        /// <summary>
        /// The top bar.
        /// </summary>
        private RelativeLayout _topBar;

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Views.MainView"/> class.
        /// </summary>
        public MainView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        /// Implement to create the layout on the page
        /// </summary>
        /// <returns>The layout.</returns>
        protected override View CreateContents()
        {
            _bottomBar = new RelativeLayout();

            var bar = new NControlView
            { 
                BackgroundColor = global::Xamarin.Forms.Color.FromHex("#EEEEEE"),
                DrawingFunction = (ICanvas canvas, Rect rect) =>
                {
                    canvas.DrawLine(0, 0, rect.Width, 0, NGraphics.Colors.Gray, 0.5);
                }
            };
            
            var buttonBar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 11,
                Children =
                {
                    new CircularButtonControl(),
                    new CircularButtonControl(),
                    new CircularButtonControl(),
                    new CircularButtonControl(),
                }
            };

            _bottomBar.Children.Add(bar, () => new global::Xamarin.Forms.Rectangle(0, 0, _bottomBar.Width, _bottomBar.Height));
            _bottomBar.Children.Add(buttonBar, () => new global::Xamarin.Forms.Rectangle(0, 0, _bottomBar.Width, _bottomBar.Height));

            _topBar = new RelativeLayout();
            var topBarBg = new NControlView{ 
                BackgroundColor = global::Xamarin.Forms.Color.FromHex("#FFFFFF"),
                DrawingFunction = (ICanvas canvas, Rect rect) =>
                    {
                        canvas.DrawLine(0, rect.Height-0.5, rect.Width, rect.Height-0.5, NGraphics.Colors.Gray, 0.5);
                    }
            };

            var topBarLabel = new Label{ 
                Text = Strings.AppName, 
                BackgroundColor = global::Xamarin.Forms.Color.Transparent,
                XAlign = global::Xamarin.Forms.TextAlignment.Center, 
                YAlign = global::Xamarin.Forms.TextAlignment.Center 
            };

            _topBar.Children.Add(topBarBg, () => _topBar.Bounds);
            _topBar.Children.Add(topBarLabel, () => _topBar.Bounds);


            var layout = new RelativeLayout();
            layout.Children.Add(_bottomBar, () => new global::Xamarin.Forms.Rectangle(0, layout.Height, layout.Width, 65));
            layout.Children.Add(_topBar, () => new global::Xamarin.Forms.Rectangle(0, -4, layout.Width, 44));

            return layout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _bottomBar.TranslateTo(0, -_bottomBar.Height, 550, Easing.BounceOut);
            _topBar.TranslateTo(0, 15, 550, Easing.BounceOut);
        }
    }
}

