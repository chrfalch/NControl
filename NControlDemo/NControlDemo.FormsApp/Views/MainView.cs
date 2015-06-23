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
using NControl.Abstractions;
using Xamarin.Forms;
using NControlDemo.FormsApp.Controls;
using NGraphics;
using NControlDemo.Localization;
using System.Threading.Tasks;
using NControlDemo.FormsApp.Views;
using NControlDemo.FormsApp.ViewModels;
using Xamarin.Forms.Maps;

namespace NControlDemo.FormsApp.Views
{
    /// <summary>
    /// Main view.
    /// </summary>
    public class MainView: BaseContentsView<MainViewModel>
    {
        /// <summary>
        /// The chrome visible.
        /// </summary>
        private bool _chromeVisible = false;

        /// <summary>
        /// The map container.
        /// </summary>
        private RelativeLayout _mapContainer;

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
        /// The top background view.
        /// </summary>
        private NControlView _topBackgroundView;

        /// <summary>
        /// The bottom background view.
        /// </summary>
        private NControlView _bottomBackgroundView;

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
            _topBackgroundView = new NControlView {    
                DrawingFunction = (canvas, rect) => canvas.FillRectangle(rect, new SolidBrush(new NGraphics.Color("#3498DB")))
            };

            _bottomBackgroundView = new NControlView {
                DrawingFunction = (canvas, rect) => canvas.FillRectangle(rect, new SolidBrush(new NGraphics.Color("#3498DB")))               
            };

            var grid = new Grid();
            grid.Children.Add(new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Padding = 11,
                    Children =
                    {
                        new CircularButtonControl { FAIcon = FontAwesomeLabel.FAPlay },
                        new CircularButtonControl { FAIcon = FontAwesomeLabel.FAPlus },
                        new CircularButtonControl { FAIcon = FontAwesomeLabel.FATerminal },
                        new Button { Text = "Hello" },
                    }
                }, 0, 0);

            var buttonOverlay = new NControlView { 
                DrawingFunction = (canvas, rect) =>
                {
                    rect.Inflate(-10, -10);
                    canvas.DrawRectangle(rect, Pens.Blue, null);
                },
            };
            buttonOverlay.InputTransparent = true;

            grid.Children.Add(buttonOverlay, 0, 0);

            _bottomBar = new NControlView
            {

                BackgroundColor = Xamarin.Forms.Color.FromHex("#EEEEEE"),
                DrawingFunction = (ICanvas canvas, Rect rect) =>
                    canvas.DrawLine(0, 0, rect.Width, 0, NGraphics.Colors.Gray, 0.5)
                ,
                Content = grid
            };

            // Navigation bar
            _navigationBar = new NavigationBarEx { Title = Strings.AppName };

            // Progress controll
            _progress = new ProgressControl();

            // Map
            _mapContainer = new RelativeLayout();

            // Layout
            var layout = new RelativeLayout();
            layout.Children.Add(_mapContainer, () => layout.Bounds);
            layout.Children.Add(_topBackgroundView, () => new Xamarin.Forms.Rectangle(0, 0, layout.Width, 1 + (layout.Height / 2)));
            layout.Children.Add(_bottomBackgroundView, () => new Xamarin.Forms.Rectangle(0, layout.Height / 2, layout.Width, layout.Height / 2));
            layout.Children.Add(_bottomBar, () => new Xamarin.Forms.Rectangle(0, layout.Height, layout.Width, 65));
            layout.Children.Add(_navigationBar, () => new Xamarin.Forms.Rectangle(0, -80, layout.Width, 80));

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
            await Task.Delay(1500);

            // Introduce the navigation bar and toolbar
            await ShowChromeAsync();

            // Hide the background and remove progressbar
            await Task.WhenAll(new[] {
                _topBackgroundView.TranslateTo(0, -Height/2, 465, Easing.CubicIn),
                _bottomBackgroundView.TranslateTo(0, Height, 465, Easing.CubicIn),
                _progress.FadeTo(0, 365, Easing.CubicIn)
            });

            // Add map
            var map = new Map();
            var mapOverlay = new NControlView { 
                BackgroundColor = Xamarin.Forms.Color.Transparent,
            };
            mapOverlay.OnTouchesBegan += async (sender, e) => await ToggleChromeAsync();
            _mapContainer.Children.Add(map, () => _mapContainer.Bounds);
            _mapContainer.Children.Add(mapOverlay, () => _mapContainer.Bounds);
        }

        /// <summary>
        /// Toggles the chrome async.
        /// </summary>
        /// <returns>The chrome async.</returns>
        private Task ToggleChromeAsync()
        {
            if (_chromeVisible)
                return HideChromeAsync();

            return ShowChromeAsync();
        }

        /// <summary>
        /// Shows the chrome, ie the navigation bar and button bar
        /// </summary>
        private async Task ShowChromeAsync()
        {
            _chromeVisible = true;

            await Task.WhenAll(new []{
                _bottomBar.TranslateTo(0, -_bottomBar.Height, 550, Easing.BounceOut),
                _navigationBar.TranslateTo(0, Device.OnPlatform<int>(61, 80, 55), 550, Easing.BounceOut),
            }); 
        }

        /// <summary>
        /// Shows the chrome, ie the navigation bar and button bar
        /// </summary>
        private async Task HideChromeAsync()
        {
            _chromeVisible = false;

            await Task.WhenAll(new []{
                _bottomBar.TranslateTo(0, 0, 550, Easing.CubicIn),
                _navigationBar.TranslateTo(0, 0, 550, Easing.CubicIn),
            }); 
        }
    }
}

