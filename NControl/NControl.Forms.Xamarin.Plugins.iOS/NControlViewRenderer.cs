using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using NControl.Plugins.Abstractions;
using NControl.Plugins.iOS;
using UIKit;
using System.Collections.Generic;
using System.Linq;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Plugins.iOS
{
	/// <summary>
	/// NControlView renderer.
	/// </summary>
	public class NControlViewRenderer: ViewRenderer<NControlView, NControlNativeView>
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public new static void Init() { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnElementChanged (ElementChangedEventArgs<NControlView> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement != null) {

                if (Control == null)
                {
                    NControlNativeView control = null;

                    if(e.NewElement.DrawingFunction == null)
                        control = new NControlNativeView((NGraphics.ICanvas canvas, NGraphics.Rect rect) => e.NewElement.Draw(canvas, rect));
                    else
                        control = new NControlNativeView(e.NewElement.DrawingFunction);

                    control.TouchesBeganEvent += HandleTouchesBeganEvent;
                    control.TouchesMovedEvent += HandleTouchesMovedEvent;
                    control.TouchesEndedEvent += HandleTouchesEndedEvent;
                    control.TouchesCancelledEvent += HandleTouchesCancelledEvent;

                    SetNativeControl(control);
                }
			}
		}

		/// <summary>
		/// Raises the element property changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged (sender, e);

			if (e.PropertyName == NControlView.CancelDefaultDrawingProperty.PropertyName)
				(Control as NControlNativeView).CancelDefaultDrawing = Element.CancelDefaultDrawing;
			else if(e.PropertyName == NControlView.TransparentProperty.PropertyName)
				(Control as NControlNativeView).Transparent = Element.CancelDefaultDrawing;
            else if(e.PropertyName == NControlView.BackgroundColorProperty.PropertyName)
                (Control as NControlNativeView).BackgroundColor = Element.BackgroundColor.ToUIColor();
		}

        #region Touch Handlers

        /// <summary>
        /// Handles the touches cancelled event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleTouchesCancelledEvent (object sender, IEnumerable<UITouch> e)
        {
            if (Element == null)
                return;

            Element.TouchesCancelled (e.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Handles the touches began event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleTouchesBeganEvent (object sender, IEnumerable<UITouch> e)
        {
            if (Element == null)
                return;

            Element.TouchesBegan (e.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Handles the touches ended event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void HandleTouchesEndedEvent (object sender, IEnumerable<UITouch> e)
        {
            if (Element == null)
                return;

            Element.TouchesEnded (e.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Handles the touches moved event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void HandleTouchesMovedEvent (object sender, IEnumerable<UITouch> e)
        {
            if (Element == null)
                return;

            Element.TouchesMoved (e.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));    
        }

        #endregion
	}
}


