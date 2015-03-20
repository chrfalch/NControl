using System;
using Xamarin.Forms;
using NGraphics;
using System.Collections.Generic;

namespace NControl.Plugins.Abstractions
{
	/// <summary>
	/// NControlView Definition
	/// </summary>
	public class NControlView: View
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/> class.
		/// </summary>
		public NControlView()
		{
			Transparent = true;
			CancelDefaultDrawing = false;
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
		/// The cancel default drawing property.
		/// </summary>
		public static BindableProperty CancelDefaultDrawingProperty = BindableProperty.Create<NControlView, bool> (w => w.CancelDefaultDrawing, false);

		/// <summary>
		/// The transparent property.
		/// </summary>
		public static BindableProperty TransparentProperty = BindableProperty.Create<NControlView, bool> (w => w.Transparent, true);

		/// <summary>
		/// Gets or sets a value indicating whether this instance cancelels default drawing.
		/// </summary>
		/// <value><c>true</c> if this instance cancel default drawing; otherwise, <c>false</c>.</value>
		public bool CancelDefaultDrawing 
		{ 
			get{ return (bool)GetValue (CancelDefaultDrawingProperty);} 
			set{ SetValue (CancelDefaultDrawingProperty, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="NControl.Forms.Xamarin.Plugins.iOS.NControlNativeView"/>
		/// is transparent.
		/// </summary>
		/// <value><c>true</c> if transparent; otherwise, <c>false</c>.</value>
		public bool Transparent 
		{ 
			get{ return (bool)GetValue (TransparentProperty);} 
			set{ SetValue (TransparentProperty, value); }
		}

		/// <summary>
		/// Gets the drawing function.
		/// </summary>
		/// <value>The drawing function.</value>
        public Action<ICanvas, Rect> DrawingFunction {get; private set;}

		#endregion

		/// <summary>
		/// Draw the specified canvas.
		/// </summary>
		/// <param name="canvas">Canvas.</param>
        public virtual void Draw(ICanvas canvas, Rect rect)
		{
		}

        /// <summary>
        /// Touchs down.
        /// </summary>
        /// <param name="point">Point.</param>
        public virtual void TouchesBegan(IEnumerable<NGraphics.Point> points)
        {
        }

        /// <summary>
        /// Toucheses the moved.
        /// </summary>
        /// <param name="point">Point.</param>
        public virtual void TouchesMoved(IEnumerable<NGraphics.Point> points)
        {
        }

        /// <summary>
        /// Toucheses the cancelled.
        /// </summary>
        public virtual void TouchesCancelled(IEnumerable<NGraphics.Point> points)
        {
        }

        /// <summary>
        /// Toucheses the ended.
        /// </summary>
        public virtual void TouchesEnded(IEnumerable<NGraphics.Point> points)
        {
        }
	}
}

