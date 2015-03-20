using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Renderers;

[assembly: ExportRenderer (typeof (FontAwesomeLabel), typeof (FontAwesomeLabelRenderer))]
namespace NControlDemo.Forms.Xamarin.Plugins.Droid.Renderers
{
    public class FontAwesomeLabelRenderer: LabelRenderer
    {
        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<global::Xamarin.Forms.Label> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            var typeface = Typeface.CreateFromAsset(global::Xamarin.Forms.Forms.Context.Assets, "Fonts/FontAwesome.ttf");
            Control.SetTypeface(typeface, TypefaceStyle.Normal);

        }
    }
}

