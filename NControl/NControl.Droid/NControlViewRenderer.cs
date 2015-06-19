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
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using NControl.Abstractions;
using NControl.Droid;
using NGraphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Droid
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

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == NControlView.BackgroundColorProperty.PropertyName)
                Element.Invalidate();
        }

        #region Native Drawing 

        /// <Docs>The Canvas to which the View is rendered.</Docs>
        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw (Android.Graphics.Canvas canvas)
        {
            // Should we clip?
            if(Element != null && Element.IsClippedToBounds)
                canvas.ClipRect(new Android.Graphics.Rect(0, 0, Width, Height), Region.Op.Replace);

            // Perform custom drawing
            var ncanvas = new CanvasCanvas(canvas);
            Element.Draw(ncanvas, new NGraphics.Rect(0, 0, Width, Height));

            // Draw elements/children etc.
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
            var touchInfo = new[]{ 
                new NGraphics.Point(e.GetX(),e.GetY())
            };

            var result = false;

            // Handle touch actions
            switch (e.Action)
            {
                case MotionEventActions.Down:
                if (Element != null)
                    result = Element.TouchesBegan(touchInfo);
                    break;

                case MotionEventActions.Move:
                if (Element != null)
                    result = Element.TouchesMoved(touchInfo);
                    break;

                case MotionEventActions.Up:
                if (Element != null)
                    result = Element.TouchesEnded(touchInfo);
                    break;          

                case MotionEventActions.Cancel:
                if (Element != null)
                    result = Element.TouchesCancelled(touchInfo);
                    break;
            }

            System.Diagnostics.Debug.WriteLine("OnTouchEvent: " + e.Action.ToString() + 
                " for " + Element.GetType().Name + " returning " + result);

            return result;
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
            Invalidate();
        }

        /// <summary>
        /// Gets the size of the screen.
        /// </summary>
        /// <returns>The screen size.</returns>
        protected Xamarin.Forms.Size GetScreenSize ()
        {           
            var metrics = Forms.Context.Resources.DisplayMetrics;
            return new Xamarin.Forms.Size (metrics.WidthPixels, metrics.HeightPixels);
        }
        #endregion
    }
}

