using System;
using NControl.Plugins.Abstractions;
using NGraphics;
using Xamarin.Forms;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace NControlDemo.FormsApp.Controls
{
    /// <summary>
    /// Circular button control.
    /// </summary>
    public class CircularButtonControl: NControlView
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls.CircularButtonControl"/> class.
        /// </summary>
        public CircularButtonControl()
        {
            HeightRequest = 44;
            WidthRequest = 44;

            Content = new FontAwesomeLabel {
                Text = FontAwesomeLabel.FAAdjust,
                TextColor = Xamarin.Forms.Color.White,
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                XAlign = Xamarin.Forms.TextAlignment.Center,
                YAlign = Xamarin.Forms.TextAlignment.Center,
            };
        }

        /// <summary>
        /// Gets or sets the FA icon.
        /// </summary>
        /// <value>The FA icon.</value>
        public string FAIcon
        {
            get
            {
                return (Content as FontAwesomeLabel).Text;
            }
            set
            {
                (Content as FontAwesomeLabel).Text = value;
            }
        }


        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="rect">Rect.</param>
        public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
        {
            base.Draw(canvas, rect);

            canvas.FillEllipse(rect, Colors.LightGray);
            rect.Inflate(new NGraphics.Size(-2, -2));
            canvas.FillEllipse(rect, Colors.White);
            rect.Inflate(new NGraphics.Size(-4, -4));
            canvas.FillEllipse(rect, Colors.LightGray);

        }

        public override async void TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);
            await this.ScaleTo(0.8, 65, Easing.CubicInOut);
        }

        public override void TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);
            TouchesEnded(points);
        }

        public override async void TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);
            await this.ScaleTo(1.0, 65, Easing.CubicInOut);
        }
    }
}

