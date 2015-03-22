using System;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels;
using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NGraphics;
using NControl.Plugins.Abstractions;
using NControlDemo.Forms.Xamarin.Plugins.Localization;
using System.Threading.Tasks;

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
        private NControlView _bottomBar;

        /// <summary>
        /// The top bar.
        /// </summary>
        private NControlView _navigationBar;

        /// <summary>
        /// The background view.
        /// </summary>
        private NControlView _backgroundView;

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
            _backgroundView = new NControlView
            {
                BackgroundColor = global::Xamarin.Forms.Color.FromHex("#3498DB"),
            };

            _bottomBar = new NControlView {
                
                BackgroundColor = global::Xamarin.Forms.Color.FromHex("#EEEEEE"),
                DrawingFunction = (ICanvas canvas, Rect rect) =>
                {
                    canvas.DrawLine(0, 0, rect.Width, 0, NGraphics.Colors.Gray, 0.5);
                },
                Content = new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 11,
                    Children =
                    {
                        new CircularButtonControl {FAIcon = FontAwesomeLabel.FAPlay },
                        new CircularButtonControl {FAIcon = FontAwesomeLabel.FAPlus },
                        new CircularButtonControl {FAIcon = FontAwesomeLabel.FATerminal },
                        new CircularButtonControl {FAIcon = FontAwesomeLabel.FAHospitalO },
                    }
                }
            };
                        
            // Navigation bar
            _navigationBar = new NavigationBarEx {Title = Strings.AppName};

            // Layout
            var layout = new RelativeLayout();
            layout.Children.Add(_backgroundView, () => layout.Bounds);
            layout.Children.Add(_bottomBar, () => new global::Xamarin.Forms.Rectangle(0, layout.Height, layout.Width, 65));
            layout.Children.Add(_navigationBar, () => new global::Xamarin.Forms.Rectangle(0, -65, layout.Width, 65));

            return layout;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Task.WhenAll(new []{
                _bottomBar.TranslateTo(0, -_bottomBar.Height + (Device.OnPlatform<int>(0,-65,0)), 550, Easing.BounceOut),
                _navigationBar.TranslateTo(0, Device.OnPlatform<int>(65, 44, 25), 550, Easing.BounceOut),
            });

            await _backgroundView.ScaleTo(0.0, 300, Easing.CubicIn);
        }
    }
}

