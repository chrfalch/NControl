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
        private readonly Label _label;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls.CircularButtonControl"/> class.
        /// </summary>
        public CircularButtonControl()
        {
            HeightRequest = 44;
            WidthRequest = 44;

            _label = new FontAwesomeLabel
            {
                Text = FontAwesomeLabel.FAAdjust,
                TextColor = Xamarin.Forms.Color.White,
                FontSize = 17,
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                XAlign = Xamarin.Forms.TextAlignment.Center,
                YAlign = Xamarin.Forms.TextAlignment.Center,
            };
            

            Content = new Grid{
                Children =
                {
                    new NControlView
                    {
                        DrawingFunction = (canvas1, rect) => {
                            canvas1.FillEllipse(rect, Colors.LightGray);
                            rect.Inflate(new NGraphics.Size(-2, -2));
                            canvas1.FillEllipse(rect, Colors.White);
                            rect.Inflate(new NGraphics.Size(-4, -4));
                            canvas1.FillEllipse(rect, Colors.LightGray);
                        }    
                    },
                    _label,
                }
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
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }


        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="rect">Rect.</param>
        //public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
        //{
        //    base.Draw(canvas, rect);

            

        //}

        public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesBegan(points);
            this.ScaleTo(0.8, 65, Easing.CubicInOut);
            return true;
        }

        public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesCancelled(points);
            TouchesEnded(points);
            return true;
        }

        public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
        {
            base.TouchesEnded(points);
            this.ScaleTo(1.0, 65, Easing.CubicInOut);
            return true;
        }
    }
}

