using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using NGraphics;
using CoreGraphics;
using NControl.Plugins.Abstractions;
using NControl.Plugins.iOS;

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
	}

}


