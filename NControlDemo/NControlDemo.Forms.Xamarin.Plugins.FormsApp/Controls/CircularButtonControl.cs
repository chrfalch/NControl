using System;
using NControl.Plugins.Abstractions;
using NGraphics;
using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls
{
    /// <summary>
    /// Circular button control.
    /// </summary>
    public class CircularButtonControl: NControlView
    {
        /// <summary>
        /// The fontawesomefont.
        /// </summary>
        private readonly NGraphics.Font _fontawesomeFont;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls.CircularButtonControl"/> class.
        /// </summary>
        public CircularButtonControl()
        {
            _fontawesomeFont = new NGraphics.Font{
                Family = "helvetica",
                Size = 22,
            };

            HeightRequest = 44;
            WidthRequest = 44;
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

        public override void TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);

            this.ScaleTo(0.8, 300, Easing.SpringOut);
        }

        public override void TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);
            this.ScaleTo(1.0, 250, Easing.SpringIn);
        }

        public override void TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);
            this.ScaleTo(1.0, 250, Easing.SpringIn);
        }
    }
}

