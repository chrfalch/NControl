using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Graphics;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.Renderers;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.Controls;

[assembly: ExportRenderer (typeof (RoundedBorderControl), typeof (RoundedBorderControlRenderer))]
namespace NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.Renderers
{
    public class RoundedBorderControlRenderer: ViewRenderer<RoundedBorderControl, RoundedBorderView>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clooger.iOS.Renderers.RoundedBorderControlRenderer"/> class.
        /// </summary>
        public RoundedBorderControlRenderer()
        {
            this.SetWillNotDraw(false);
        }

        #region Overridden Members

        /// <Docs>The Canvas to which the View is rendered.</Docs>
        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw(Canvas canvas)
        {
            var rbv = (RoundedBorderControl)this.Element;

            Rect rc = new Rect();
            GetDrawingRect(rc);

            var p = new Paint(PaintFlags.AntiAlias);
            p.Color = rbv.BackgroundColor.ToAndroid();
            p.SetStyle(Paint.Style.Fill);

            canvas.DrawRoundRect(new RectF(rc), (float)rbv.CornerRadius, (float)rbv.CornerRadius, p);

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
                Control.CornerRadius = Element.CornerRadius;
        }

        #endregion

    }
}

