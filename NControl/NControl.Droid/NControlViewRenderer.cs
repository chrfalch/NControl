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
using System.Collections.Generic;
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
    [Preserve(AllMembers = true)]
    public class NControlViewRenderer : VisualElementRenderer<NControlView>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init ()
        {
            var temp = DateTime.Now;
        }

        /// <summary>
        /// Raises the element changed event.
        /// </summary>
        /// <param name="e">E.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<NControlView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                e.OldElement.OnInvalidate -= HandleInvalidate;
                e.OldElement.OnGetPlatform -= OnGetPlatformHandler;
            }

            if (e.NewElement != null)
            {
                e.NewElement.OnInvalidate += HandleInvalidate;
                e.NewElement.OnGetPlatform += OnGetPlatformHandler;
            }

            // Lets have a clear background
            this.SetBackgroundColor (Android.Graphics.Color.Transparent);

            Invalidate();
        }

        /// <summary>
        /// Override to avoid setting the background to a given color
        /// </summary>
        protected override void UpdateBackgroundColor()
        {
            // Do NOT call update background here.
            // base.UpdateBackgroundColor();
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
            else if (e.PropertyName == NControlView.IsVisibleProperty.PropertyName)
                Element.Invalidate();
        }

        protected override void Dispose (bool disposing)
        {
            if (disposing && Element != null)
            {
                Element.OnInvalidate -= HandleInvalidate;
                Element.OnGetPlatform -= OnGetPlatformHandler;
            }
            base.Dispose (disposing);
        }

        #region Native Drawing 

        /// <Docs>The Canvas to which the View is rendered.</Docs>
        /// <summary>
        /// Draw the specified canvas.
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        public override void Draw(Android.Graphics.Canvas canvas)
        {
            if (Element == null)
            {               
                base.Draw(canvas);
                return;
            }

            // Draws the background and default android setup. Children will also be redrawn here
            // base.Draw(canvas);

            // Set up clipping
            if (Element.IsClippedToBounds)
                canvas.ClipRect(new Android.Graphics.Rect(0, 0, Width, Height));

            // Perform custom drawing from the NGraphics subsystems
            var ncanvas = new CanvasCanvas(canvas);

            var rect = new NGraphics.Rect(0, 0, Width, Height);

            // Fill background 
            ncanvas.FillRectangle(rect, new NGraphics.Color(Element.BackgroundColor.R, Element.BackgroundColor.G, Element.BackgroundColor.B, Element.BackgroundColor.A));

            // Custom drawing
            Element.Draw(ncanvas, rect);

            // Redraw children - since we might have a composite control containing both children 
            // and custom drawing code, we want children to be drawn last. The reason for this double-
            // drawing is that the base.Draw(canvas) call will handle background which is needed before
            // doing NGraphics drawing - but unfortunately it also draws children - which then will 
            // be drawn below NGraphics drawings.
            base.Draw(canvas);
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
            var scale = Element.Width / Width;

            var touchInfo = new List<NGraphics.Point>();
            for (var i = 0; i < e.PointerCount; i++)
            {
                var coord = new MotionEvent.PointerCoords();
                e.GetPointerCoords(i, coord);
                touchInfo.Add(new NGraphics.Point(coord.X*scale, coord.Y*scale));
            }

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
        /// Callback for the OnGetPlatform event in the abstract control
        /// </summary>
        private IPlatform OnGetPlatformHandler ()
        {
            return new AndroidPlatform();
        }

        /// <summary>
        /// Gets the size of the screen.
        /// </summary>
        /// <returns>The screen size.</returns>
        protected Xamarin.Forms.Size GetScreenSize()
        {
            var metrics = Forms.Context.Resources.DisplayMetrics;
            return new Xamarin.Forms.Size(metrics.WidthPixels, metrics.HeightPixels);
        }
        #endregion
    }
}

