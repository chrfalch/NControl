using System;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.Renderers;

[assembly: ExportRenderer (typeof (RoundedBorderControl), typeof (RoundedBorderControlRenderer))]
namespace NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.Renderers
{
    public class RoundedBorderControlRenderer: ViewRenderer<RoundedBorderControl, UIView>
    {
        #region Overridden Members

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<RoundedBorderControl> e)
        {
            base.OnElementChanged(e);

            Unbind(e.OldElement);
            Bind(e.NewElement);

            if (e.OldElement == null)
            {
                var nativeView = new UIView();
                nativeView.Layer.CornerRadius = Element.CornerRadius;
                SetNativeControl(nativeView);
            }
         
        }
        #endregion

        #region Private Members

        /// <summary>
        /// Unbind
        /// </summary>
        /// <param name="oldElement">Old element.</param>
        private void Unbind (RoundedBorderControl oldElement)
        {
            if (oldElement != null)             
                oldElement.PropertyChanged -= ElementPropertyChanged;
        }

        /// <summary>
        /// Bind
        /// </summary>
        /// <param name="newElement">New element.</param>
        private void Bind (RoundedBorderControl newElement)
        {
            if (newElement != null)             
                newElement.PropertyChanged += ElementPropertyChanged;
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void ElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (Control == null)
                return;

            if (e.PropertyName == RoundedBorderControl.CornerRadiusProperty.PropertyName)
                Control.Layer.CornerRadius = Element.CornerRadius;
        }

        #endregion

    }
}

