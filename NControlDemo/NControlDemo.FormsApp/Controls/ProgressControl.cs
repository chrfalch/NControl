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

