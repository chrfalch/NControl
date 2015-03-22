using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using NControl.Plugins.Abstractions;
using NControl.Plugins.Droid;
using Android.Views;
using NGraphics;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Plugins.Droid
{
	/// <summary>
	/// NControlView renderer.
	/// </summary>
    public class NControlViewRenderer: VisualElementRenderer<NControlView>
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init() { }

        #region Native Drawing 

        /// <Docs>The Canvas to which the View is rendered.</Docs>
        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw (Android.Graphics.Canvas canvas)
        {
            var ncanvas = new CanvasCanvas(canvas);
            Element.Draw(ncanvas, new Rect(0, 0, Width, Height));

            base.Draw (canvas);

        }
        #endregion

        #region Touch Handling

        /// <Docs>The motion event.</Docs>
        /// <returns>To be added.</returns>
        /// <para tool="javadoc-to-mdoc">Implement this method to handle touch screen motion events.</para>
        /// <format type="text/html">[Android Documentation]</format>
        /// <since version="Added in API level 1"></since>
        /// <summary>
        /// Raises the touch event event.
        /// </summary>
        /// <param name="e">E.</param>
        public override bool OnTouchEvent(MotionEvent e)
        {
            var touchInfo = new NGraphics.Point[]{ 
                new NGraphics.Point{X = e.GetX(), Y = e.GetY()}
            };

            // Handle touch actions
            switch (e.Action) {

                case MotionEventActions.Down:
                    Element.TouchesBegan (touchInfo);
                    break;

                case MotionEventActions.Move:
                    Element.TouchesMoved (touchInfo);
                    break;

                case MotionEventActions.Up:
                    Element.TouchesEnded (touchInfo);
                    break;          

                case MotionEventActions.Cancel:
                    Element.TouchesCancelled (touchInfo);
                    break;
            }

            return true;
        }

        #endregion
	}
}

