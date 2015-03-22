using System;
using NControl.Plugins.Abstractions;
using Xamarin.Forms;
using NGraphics;

namespace NControlDemo.FormsApp.Controls
{
    /// <summary>
    /// Implements a progress control showing two cogwheels spinning on a 
    /// background with curved corners.
    /// </summary>
    public class ProgressControl: RoundedBorderControl
    {        
        /// <summary>
        /// The animation.
        /// </summary>
        private Animation _animation;

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.FormsApp.Controls.ProgressControl"/> class.
        /// </summary>
        public ProgressControl()
        {
            BackgroundColor = Xamarin.Forms.Color.Gray;

            var cog1 = new FontAwesomeLabel{ 
                Text = FontAwesomeLabel.FACog,
                FontSize = 39,
                TextColor = Xamarin.Forms.Color.FromHex("#CECECE"),
                XAlign = Xamarin.Forms.TextAlignment.Center,
                YAlign = Xamarin.Forms.TextAlignment.Center,
            };

            var cog2 = new FontAwesomeLabel{ 
                Text = FontAwesomeLabel.FACog,
                FontSize = 18,
                TextColor = Xamarin.Forms.Color.FromHex("#CECECE"),
                XAlign = Xamarin.Forms.TextAlignment.Center,
                YAlign = Xamarin.Forms.TextAlignment.Center,
            };
                
            _animation = new Animation((val) => {
                cog1.Rotation = val;
                cog2.Rotation = -val;
            }, 0, 360, Easing.Linear);

            var layout = new RelativeLayout();
            layout.Children.Add(cog1, () => new Xamarin.Forms.Rectangle((-layout.Width/4) + 8, (layout.Height/4) - 8, 
                layout.Width, layout.Height));
            
            layout.Children.Add(cog2, () => new Xamarin.Forms.Rectangle(layout.Width -36, -13, 
                layout.Width, layout.Width));

            Content = layout;
        }

        /// <summary>
        /// Start animating this instance.
        /// </summary>
        public void Start()
        {
            _animation.Commit(this, "Rotation", length:2000, repeat: () => true);
        }
    }
}

