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
            return new StackLayout{                
                Children = {
                    new FontAwesomeLabel{Text = FontAwesomeLabel.FAAlignJustify},
                    new NControlView((ICanvas canvas, Rect rect) => {

                        var font = new NGraphics.Font();
                        font.Family = "Arial";
                        font.Size = 14;

                        canvas.FillEllipse(rect, NGraphics.Colors.Yellow);
                        canvas.DrawLine(new NGraphics.Point(rect.X, rect.Y), new NGraphics.Point(rect.Width, rect.Height), NGraphics.Colors.Red);
                        canvas.DrawText("ABCD", new Rect(50, 50, 50, 50), font, NGraphics.TextAlignment.Left, null, Brushes.Black);

                    }){ WidthRequest = 100, HeightRequest = 100, BackgroundColor = global::Xamarin.Forms.Color.Transparent},
                }
            };
        }
    }
}

