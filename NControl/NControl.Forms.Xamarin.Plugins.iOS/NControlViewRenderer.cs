using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using NControl.Plugins.Abstractions;
using NControl.Plugins.iOS;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;
using NGraphics;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Plugins.iOS
{
	/// <summary>
	/// NControlView renderer.
	/// </summary>
    public class NControlViewRenderer: VisualElementRenderer<NControlView>
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public new static void Init() { }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<NControlView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
                e.OldElement.OnInvalidate -= HandleInvalidate;

            if (e.NewElement != null)
                e.NewElement.OnInvalidate += HandleInvalidate;
        }
        		
        #region Drawing

        /// <summary>
        /// Draw the specified rect.
        /// </summary>
        /// <param name="rect">Rect.</param>
        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            using (CGContext context = UIGraphics.GetCurrentContext ()) 
            {
                var canvas = new CGContextCanvas (context);
                Element.Draw (canvas, new NGraphics.Rect(rect.Left, rect.Top, rect.Width, rect.Height));
            }        
        }
        #endregion

        #region Touch Handlers

        /// <summary>
        /// Handles touches began
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (Element == null)
                return;

            var touchList = touches.ToArray<UITouch>();

            Element.TouchesBegan (touchList.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Handles touches moved
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (Element == null)
                return;

            var touchList = touches.ToArray<UITouch>();

            Element.TouchesMoved (touchList.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Touches ended
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (Element == null)
                return;

            var touchList = touches.ToArray<UITouch>();

            Element.TouchesEnded (touchList.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        /// <summary>
        /// Handles touches cancelled
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            if (Element == null)
                return;

            var touchList = touches.ToArray<UITouch>();

            Element.TouchesCancelled (touchList.Select(t => new NGraphics.Point{
                X = (float)t.LocationInView(t.View).X, Y = (float)t.LocationInView(t.View).Y
            }));
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Handles the invalidate.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        private void HandleInvalidate(object sender, System.EventArgs args)
        {
            SetNeedsDisplay();
        }
        #endregion
	}
}


