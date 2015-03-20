using System;
using Xamarin.Forms;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls
{
    /// <summary>
    /// Rounded border control.
    /// </summary>
    public class RoundedBorderControl: BoxView
    {
        /// <summary>
        /// The corner radius property.
        /// </summary>
        public static BindableProperty CornerRadiusProperty = 
            BindableProperty.Create<RoundedBorderControl, int> (p => p.CornerRadius, 0);

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
    }
}

