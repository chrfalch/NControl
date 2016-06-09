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
using NGraphics;

namespace NControlDemo.FormsApp.Controls
{
    /// <summary>
    /// Rounded border control.
    /// </summary>
    public class RoundedBorderControl: NControlView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.FormsApp.Controls.RoundedBorderControl"/> class.
        /// </summary>
        public RoundedBorderControl()
        {
            base.BackgroundColor = Xamarin.Forms.Color.Transparent;
        }

        #region Properties

        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static BindableProperty CornerRadiusProperty = 
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundedBorderControl), 0,
                BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                {                    
                    (bindable as RoundedBorderControl).Invalidate();
                });
        
        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public int CornerRadius 
        {
            get { return (int)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        /// <summary>
        /// The border and fill color property.
        /// </summary>
        public static new BindableProperty BackgroundColorProperty = 
            BindableProperty.Create(nameof(BackgroundColor), typeof(Xamarin.Forms.Color), typeof(RoundedBorderControl), 
                Xamarin.Forms.Color.Transparent, BindingMode.OneWay, null, (bindable, oldValue, newValue) =>
                {
                    (bindable as RoundedBorderControl).Invalidate();
                });

        /// <summary>
        /// Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.
        /// </summary>
        /// <value>The color of the background.</value>
        public new Xamarin.Forms.Color BackgroundColor
        {
            get { return (Xamarin.Forms.Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        #endregion

        #region Drawing

        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="rect">Rect.</param>
        public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
        {
            base.Draw(canvas, rect);

            var backgroundBrush = new SolidBrush(new NGraphics.Color(BackgroundColor.R,
                BackgroundColor.G, BackgroundColor.B, BackgroundColor.A));
            
            var curveSize = CornerRadius;
            var width = rect.Width;
            var height = rect.Height;
            canvas.DrawPath(new PathOp []{ 
                new MoveTo(curveSize, 0),
                // Top Right corner
                new LineTo(width-curveSize, 0),
                new CurveTo(
                    new NGraphics.Point(width-curveSize, 0),
                    new NGraphics.Point(width, 0),
                    new NGraphics.Point(width, curveSize)
                ),
                new LineTo(width, height-curveSize),
                // Bottom right corner
                new CurveTo(
                    new NGraphics.Point(width, height-curveSize),
                    new NGraphics.Point(width, height),
                    new NGraphics.Point(width-curveSize, height)
                ),
                new LineTo(curveSize, height),
                // Bottom left corner
                new CurveTo(
                    new NGraphics.Point(curveSize, height),
                    new NGraphics.Point(0, height),
                    new NGraphics.Point(0, height-curveSize)
                ),
                new LineTo(0, curveSize),
                new CurveTo(
                    new NGraphics.Point(0, curveSize),
                    new NGraphics.Point(0, 0),
                    new NGraphics.Point(curveSize, 0)
                ),
                new ClosePath()
            }, null, backgroundBrush);

        }

        #endregion
       
    }
}

