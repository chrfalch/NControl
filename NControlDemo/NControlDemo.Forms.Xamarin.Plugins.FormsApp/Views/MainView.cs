using System;
using Xamarin.Forms;
using NControlDemo.FormsApp.Controls;
using NGraphics;
using NControl.Plugins.Abstractions;
using NControlDemo.Localization;
using System.Threading.Tasks;
using NControlDemo.FormsApp.Views;
using NControlDemo.FormsApp.ViewModels;

namespace NControlDemo.FormsApp.Views
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
        /// The progress.
        /// </summary>
        private ProgressControl _progress;

        /// <summary>
        /// The background view.
        /// </summary>
        private NControlView _backgroundView;

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.FormsApp.Views.MainView"/> class.
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
            _backgroundView = new NControlView {
                BackgroundColor = Xamarin.Forms.Color.FromHex("#3498DB"),
                DrawingFunction = (canvas, rect) => {
                    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
                    canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
                }
            };

            _bottomBar = new NControlView {
                
                BackgroundColor = Xamarin.Forms.Color.FromHex("#EEEEEE"),
                DrawingFunction = (ICanvas canvas, Rect rect) => 
                    canvas.DrawLine(0, 0, rect.Width, 0, NGraphics.Colors.Gray, 0.5)
                ,
                Content = new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
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

            // Progress controll
            _progress = new ProgressControl();

            // Layout
            var layout = new RelativeLayout();
            layout.Children.Add(_backgroundView, () => layout.Bounds);
            layout.Children.Add(_bottomBar, () => new Xamarin.Forms.Rectangle(0, layout.Height, layout.Width, 65));
            layout.Children.Add(_navigationBar, () => new Xamarin.Forms.Rectangle(0, -65, layout.Width, 65));

            layout.Children.Add(_progress, () => new Xamarin.Forms.Rectangle((layout.Width / 2) - (25),
                    (layout.Height / 2) - 25, 50, 50));

            return layout;
        }

        /// <summary>
        /// Startup animations
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Start the progress control
            _progress.Start();

            // Lets pretend we're doing something
            await Task.Delay(750);

            // Introduce the navigation bar and toolbar
            await Task.WhenAll(new []{
                _bottomBar.TranslateTo(0, -_bottomBar.Height, 550, Easing.BounceOut),
                _navigationBar.TranslateTo(0, Device.OnPlatform<int>(65, 44, 25), 550, Easing.BounceOut),
            }); 

            // Wait a little bit more
            await Task.Delay(1500);

            // Hide the background and remove progressbar
            await Task.WhenAll(new [] {
                _backgroundView.FadeTo(0, 350, Easing.CubicIn),
                _progress.FadeTo(0, 65, Easing.CubicIn)
            });
        }
    }
}

