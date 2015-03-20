using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Android.Content;
using NGraphics;

namespace NControl.Plugins.Droid
{
	/// <summary>
	/// NControl native view.
	/// </summary>
	public class NControlNativeView : Android.Views.View
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
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.Droid.NControlNativeView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		public NControlNativeView(Context context): base(context)
		{
			CancelDefaultDrawing = false;
			Transparent = true;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.Droid.NControlNativeView"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
        public NControlNativeView(Action<ICanvas, Rect> drawingFunc, Context context): this(context)
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
				Invalidate();
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
				Invalidate();
			} 
		}

		#endregion

		#region Native Drawing 

		/// <Docs>The Canvas to which the View is rendered.</Docs>
		/// <summary>
		/// Draw the specified canvas.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
		public override void Draw (Android.Graphics.Canvas canvas)
		{
			if(!CancelDefaultDrawing)
				base.Draw (canvas);

			var ncanvas = new CanvasCanvas (canvas);
			if (_drawingFunc != null)
                _drawingFunc (ncanvas, new Rect(0, 0, Width, Height));
		}
		#endregion
	}

}

