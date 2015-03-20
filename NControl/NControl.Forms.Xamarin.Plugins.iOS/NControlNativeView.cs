using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using NGraphics;
using CoreGraphics;
using NControl.Plugins.Abstractions;
using NControl.Plugins.iOS;
using System.Collections.Generic;
using Foundation;

namespace NControl.Plugins.iOS
{
	/// <summary>
	/// NControl native view.
	/// </summary>
	public class NControlNativeView : UIView
	{
		#region Private Members

		/// <summary>
		/// The drawing function.
		/// </summary>
		private readonly Action<ICanvas, Rect> _drawingFunc;

		/// <summary>
		/// The cancel default drawing.
		/// </summary>
		private bool _cancelDefaultDrawing;

		/// <summary>
		/// The transparent.
		/// </summary>
		private bool _transparent;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/> class.
		/// </summary>
		public NControlNativeView()
		{
			Transparent = true;
			CancelDefaultDrawing = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/> class with
		/// a callback action for drawing
		/// </summary>
		/// <param name="drawingFunc">Drawing func.</param>
        public NControlNativeView(Action<ICanvas, Rect> drawingFunc): this()
		{
			_drawingFunc = drawingFunc;
		}
            
		#endregion

        #region Events

        /// <summary>
        /// Occurs when touches began.
        /// </summary>
        public event EventHandler<IEnumerable<UITouch>> TouchesBeganEvent;

        /// <summary>
        /// Occurs when touches moved.
        /// </summary>
        public event EventHandler<IEnumerable<UITouch>> TouchesMovedEvent;

        /// <summary>
        /// Occurs when touches ended.
        /// </summary>
        public event EventHandler<IEnumerable<UITouch>> TouchesEndedEvent;

        /// <summary>
        /// Occurs when touches cancelled.
        /// </summary>
        public event EventHandler<IEnumerable<UITouch>> TouchesCancelledEvent;

        #endregion

		#region Properties

		/// <summary>
		/// Gets or sets a value indicating whether this instance cancelels default drawing.
		/// </summary>
		/// <value><c>true</c> if this instance cancel default drawing; otherwise, <c>false</c>.</value>
		public bool CancelDefaultDrawing 
		{ 
			get
			{
				return _cancelDefaultDrawing;
			} 
			set
			{
				if (_cancelDefaultDrawing == value)
					return;

				_cancelDefaultDrawing = value;
				SetNeedsDisplay ();
			} 
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/>
		/// is transparent.
		/// </summary>
		/// <value><c>true</c> if transparent; otherwise, <c>false</c>.</value>
		public bool Transparent 
		{
			get
			{
				return _transparent;
			} 
			set
			{
				if (_transparent == value)
					return;

				_transparent = value;
				SetNeedsDisplay ();
			} 
		}

		#endregion

		#region Native Drawing 

		/// <summary>
		/// Draw the specified rect.
		/// </summary>
		/// <param name="rect">Rect.</param>
		public override void Draw (CoreGraphics.CGRect rect)
		{
			if(!CancelDefaultDrawing)
				base.Draw (rect);

            using (CGContext context = UIGraphics.GetCurrentContext ()) 
			{
				var canvas = new CGContextCanvas (context);

				if (_drawingFunc != null)
                    _drawingFunc (canvas, new NGraphics.Rect(rect.Left, rect.Top, rect.Width, rect.Height));
			}
		}

		#endregion

        #region Native Touch

        /// <summary>
        /// Toucheses the began.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesBegan (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesBegan (touches, evt);

            if (TouchesBeganEvent != null)
                TouchesBeganEvent (this, touches.ToArray<UITouch>());
        }

        /// <summary>
        /// Toucheses the cancelled.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesCancelled (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled (touches, evt);

            if (TouchesCancelledEvent != null)
                TouchesCancelledEvent (this, touches.ToArray<UITouch>());
        }

        /// <summary>
        /// Toucheses the ended.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesEnded (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesEnded (touches, evt);

            if (TouchesEndedEvent != null)
                TouchesEndedEvent (this, touches.ToArray<UITouch>());
        }

        /// <summary>
        /// Toucheses the moved.
        /// </summary>
        /// <param name="touches">Touches.</param>
        /// <param name="evt">Evt.</param>
        public override void TouchesMoved (Foundation.NSSet touches, UIEvent evt)
        {
            base.TouchesMoved (touches, evt);

            if (TouchesMovedEvent != null)
                TouchesMovedEvent (this, touches.ToArray<UITouch>());
        }

        #endregion
	}

}


