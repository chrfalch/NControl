using System;
using NControl.Plugins.Abstractions;
using Xamarin.Forms;
using NGraphics;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls
{
    /// <summary>
    /// Navigation bar ex.
    /// </summary>
    public class NavigationBarEx: NControlView
    {
        /// <summary>
        /// The title label.
        /// </summary>
        private Label _titleLabel;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls.NavigationBarEx"/> class.
        /// </summary>
        public NavigationBarEx()
        {
            BackgroundColor = global::Xamarin.Forms.Color.FromHex("FFFFFF");
            //BackgroundColor = global::Xamarin.Forms.Color.FromHex("#3498db");
            _titleLabel = new Label { 
                Text = "", 
                //TextColor = global::Xamarin.Forms.Color.White,
                BackgroundColor = global::Xamarin.Forms.Color.Transparent,
                XAlign = global::Xamarin.Forms.TextAlignment.Center, 
                YAlign = global::Xamarin.Forms.TextAlignment.Center 
            };

            var layout = new RelativeLayout();
            layout.Children.Add(_titleLabel, () => new global::Xamarin.Forms.Rectangle(0, layout.Height - 44, layout.Width, 44));
            Content = layout;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title 
        {
            get { return _titleLabel.Text; }
            set { _titleLabel.Text = value; }
        }

        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="rect">Rect.</param>
        public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
        {
            base.Draw(canvas, rect);
            canvas.DrawLine(0, rect.Height-0.5, rect.Width, rect.Height-0.5, NGraphics.Colors.Gray, 0.5);
        }
    }
}

