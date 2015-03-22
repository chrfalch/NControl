using System;
using Xamarin.Forms;
using NControl.Plugins.Abstractions;
using NGraphics;

namespace NControlDemo.FormsApp.Controls
{
    /// <summary>
    /// Rounded border control.
    /// </summary>
    public class RoundedBorderControl: NControlView
    {
        #region Private Members

        /// <summary>
        /// The color of the background.
        /// </summary>
        private Xamarin.Forms.Color _backgroundColor;

        #endregion

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
            BindableProperty.Create<RoundedBorderControl, int> (p => p.CornerRadius, 8);

        /// <summary>
        /// Gets or sets the corner radius.
        /// </summary>
        /// <value>The corner radius.</value>
        public int CornerRadius 
        {
            get
            {
                return (int)GetValue(CornerRadiusProperty);
            }
            set
            {
                SetValue(CornerRadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color which will fill the background of a VisualElement. This is a bindable property.
        /// </summary>
        /// <value>The color of the background.</value>
        public new Xamarin.Forms.Color BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
                Invalidate();
            }
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

            var backgroundBrush = new SolidBrush(new NGraphics.Color(_backgroundColor.R,
                _backgroundColor.G, _backgroundColor.B, _backgroundColor.A));
            
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

