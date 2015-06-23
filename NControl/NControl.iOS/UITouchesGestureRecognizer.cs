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
using System;
using System.Collections.Generic;

[assembly: ExportRenderer (typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.iOS
{
    /// <summary>
    /// User interface touches gesture recognizer.
    /// </summary>
    public sealed class UITouchesGestureRecognizer : UIGestureRecognizer
    {
        #region Private Members

        /// <summary>
        /// The element.
        /// </summary>
        private NControlView _element;

        /// <summary>
        /// The native view.
        /// </summary>
        private UIView _nativeView;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NControl.iOS.UITouchesGestureRecognizer"/> class.
        /// </summary>
        /// <param name="element">Element.</param>
        /// <param name="nativeView">Native view.</param>
        public UITouchesGestureRecognizer(NControlView element, UIView nativeView)
        {
            if (null == element)
            {
                throw new ArgumentNullException("element");
            }

            if (null == nativeView)
            {
                throw new ArgumentNullException("nativeView");
            }

            _element = element;
            _nativeView = nativeView;
        }

        /// <summary>
        /// Gets the touch points.
        /// </summary>
        /// <returns>The touch points.</returns>
        /// <param name="touches">Touches.</param>
        private IEnumerable<NGraphics.Point> GetTouchPoints(
            Foundation.NSSet touches)
        {
            var points = new List<NGraphics.Point>((int)touches.Count);
            foreach (UITouch touch in touches)
            {
                CGPoint touchPoint = touch.LocationInView(_nativeView);
                points.Add(new NGraphics.Point((double)touchPoint.X, (double)touchPoint.Y));
            }

            return points;
        }

        /// <summary>
        /// Toucheses the began.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesBegan(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            if (this._element.TouchesBegan(GetTouchPoints(touches)))
                this.State = UIGestureRecognizerState.Began;
            else
                this.State = UIGestureRecognizerState.Cancelled;
        }

        /// <summary>
        /// Toucheses the moved.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesMoved(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            if (this._element.TouchesMoved(GetTouchPoints(touches)))
                this.State = UIGestureRecognizerState.Changed;            
        }

        /// <summary>
        /// Toucheses the ended.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesEnded(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded(touches, evt);

            if (this._element.TouchesEnded(GetTouchPoints(touches)))
                this.State = UIGestureRecognizerState.Ended;            
        }

        /// <summary>
        /// Toucheses the cancelled.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesCancelled(Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            this._element.TouchesCancelled(GetTouchPoints(touches));
            this.State = UIGestureRecognizerState.Cancelled;
        }
    }
}


