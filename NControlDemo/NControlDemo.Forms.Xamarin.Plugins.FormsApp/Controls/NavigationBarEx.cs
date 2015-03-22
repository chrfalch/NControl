using System;
using NControl.Plugins.Abstractions;
using Xamarin.Forms;
using NGraphics;

namespace NControlDemo.FormsApp.Controls
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
            BackgroundColor = Xamarin.Forms.Color.FromHex("FFFFFF");
            _titleLabel = new Label { 
                Text = "", 
                BackgroundColor = Xamarin.Forms.Color.Transparent,
                XAlign = Xamarin.Forms.TextAlignment.Center, 
                YAlign = Xamarin.Forms.TextAlignment.Center 
            };

            var layout = new RelativeLayout();
            layout.Children.Add(_titleLabel, () => new Xamarin.Forms.Rectangle(0, layout.Height - 44, layout.Width, 44));
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

