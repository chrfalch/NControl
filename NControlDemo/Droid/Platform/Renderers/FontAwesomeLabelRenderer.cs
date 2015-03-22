using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using NControlDemo.FormsApp.Controls;
using NControlDemo.Droid.Renderers;

[assembly: ExportRenderer (typeof (FontAwesomeLabel), typeof (FontAwesomeLabelRenderer))]
namespace NControlDemo.Droid.Renderers
{
    public class FontAwesomeLabelRenderer: LabelRenderer
    {
        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            var typeface = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/FontAwesome.ttf");
            Control.SetTypeface(typeface, TypefaceStyle.Normal);

        }
    }
}

