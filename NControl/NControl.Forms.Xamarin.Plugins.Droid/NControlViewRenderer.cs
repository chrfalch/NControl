using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using NControl.Plugins.Abstractions;
using NControl.Plugins.Droid;
using Android.Views;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Plugins.Droid
{
	/// <summary>
	/// NControlView renderer.
	/// </summary>
	public class NControlViewRenderer: ViewRenderer<NControlView, NControlNativeView>
	{
		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public static void Init() { }

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
                        control = new NControlNativeView((NGraphics.ICanvas canvas, NGraphics.Rect rect) => e.NewElement.Draw(canvas, rect), Context);
                    else
                        control = new NControlNativeView(e.NewElement.DrawingFunction, Context);

                    control.Touch += HandleTouch;
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
		}

        /// <summary>
        /// Handles the touch.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void HandleTouch (object sender, TouchEventArgs e)
        {
            if (Control == null)
                return;

            var touchInfo = new NGraphics.Point[]{ 
                new NGraphics.Point{X = e.Event.GetX(), Y = e.Event.GetY()}
            };

            // Handle touch actions
            switch (e.Event.Action) {

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
        }
	}
}

