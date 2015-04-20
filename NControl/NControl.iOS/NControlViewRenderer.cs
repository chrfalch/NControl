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

using System.Linq;
using CoreGraphics;
using NControl.Abstractions;
using NControl.iOS;
using NGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.iOS
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
            {
                e.NewElement.OnInvalidate += HandleInvalidate;                
            }
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NControlView.IsClippedToBoundsProperty.PropertyName)
                Layer.MasksToBounds = Element.IsClippedToBounds;
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
                context.SetAllowsAntialiasing(true);
                context.SetShouldAntialias(true);
                context.SetShouldSmoothFonts(true);

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


