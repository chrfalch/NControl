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

using System;
using System.Reflection;
using System.Collections.Generic;
using NGraphics;
using Xamarin.Forms;

namespace NControl.Abstractions
{
	/// <summary>
	/// NControlView Definition
	/// </summary>
    public class NControlView: ContentView
	{
        #region Events

        /// <summary>
        /// Occurs when on invalidate.
        /// </summary>
        public event System.EventHandler OnInvalidate;

        /// <summary>
        /// Touches began
        /// </summary>
        public event System.EventHandler<IEnumerable<NGraphics.Point>> OnTouchesBegan;

        /// <summary>
        /// Touches moved
        /// </summary>
        public event System.EventHandler<IEnumerable<NGraphics.Point>> OnTouchesMoved;

        /// <summary>
        /// Touches ended
        /// </summary>
        public event System.EventHandler<IEnumerable<NGraphics.Point>> OnTouchesEnded;

        /// <summary>
        /// Touches cancelled
        /// </summary>
        public event System.EventHandler<IEnumerable<NGraphics.Point>> OnTouchesCancelled;

        #endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/> class.
		/// </summary>
		public NControlView()
		{
			BackgroundColor = Xamarin.Forms.Color.Transparent;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/> class with
		/// a callback action for drawing
		/// </summary>
		/// <param name="drawingFunc">Drawing func.</param>
        public NControlView(Action<ICanvas, Rect> drawingFunc): this()
		{
			DrawingFunction = drawingFunc;
		}

		#endregion

        #region Properties

	    /// <summary>
	    /// Gets the drawing function.
	    /// </summary>
	    /// <value>The drawing function.</value>	    
	    public Action<ICanvas, Rect> DrawingFunction { get; set; }       

		#endregion

        #region Drawing

        /// <summary>
        /// Invalidate this instance.
        /// </summary>
        public void Invalidate()
        {
            if (OnInvalidate != null)
                OnInvalidate(this, EventArgs.Empty);
        }

		/// <summary>
		/// Draw the specified canvas.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
        public virtual void Draw(ICanvas canvas, Rect rect)
		{
            if (DrawingFunction != null)
                DrawingFunction(canvas, rect);
		}

        #endregion

        #region Touches

        /// <summary>
        /// Touchs down.
        /// </summary>
        /// <param name="point">Point.</param>
        public virtual bool TouchesBegan(IEnumerable<NGraphics.Point> points)
        {
            if (OnTouchesBegan != null)
            {
                OnTouchesBegan(this, points);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Toucheses the moved.
        /// </summary>
        /// <param name="point">Point.</param>
        public virtual bool TouchesMoved(IEnumerable<NGraphics.Point> points)
        {
            if (OnTouchesMoved != null)
            {
                OnTouchesMoved(this, points);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Toucheses the cancelled.
        /// </summary>
        public virtual bool TouchesCancelled(IEnumerable<NGraphics.Point> points)
        {
            if (OnTouchesCancelled != null)
            {
                OnTouchesCancelled(this, points);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Toucheses the ended.
        /// </summary>
        public virtual bool TouchesEnded(IEnumerable<NGraphics.Point> points)
        {
            if (OnTouchesEnded != null) { 
                OnTouchesEnded(this, points);
                return true;
            }

            return false;
        }

        #endregion
    }
}

