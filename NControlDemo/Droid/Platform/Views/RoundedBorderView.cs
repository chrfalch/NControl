using System;
using Android.Views;
using Android.Content;
using Android.Graphics.Drawables;

namespace NControlDemo.Droid.Platform.Controls
{
    public class RoundedBorderView: View
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clooger.App.Android.Controls.RoundedBorderView"/> class.
        /// </summary>
        /// <param name="context">Context.</param>
        public RoundedBorderView(Context context): base(context)
        {
        }

        public int CornerRadius {get;set;}
    }
}

