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

using NControl.Plugins.Abstractions;
using NControl.Plugins.WP81;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.Plugins.WP81
{    
	/// <summary>
	/// NControlView renderer.
	/// </summary>
    public class NControlViewRenderer : ViewRenderer<NControlView, NControlNativeView>
	{
        /// <summary>
        /// Canvas element
        /// </summary>
        private Canvas _canvas;

		/// <summary>
		/// Used for registration with dependency service
		/// </summary>
		public new static void Init() { }

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
            {
                e.NewElement.OnInvalidate += HandleInvalidate;                              
            }

            if (Control == null)
            {
                var b = new NControlNativeView();
                _canvas = new Canvas();
                b.Children.Add(_canvas);
                
                SetNativeControl(b);
                
                b.MouseLeftButtonDown += HandleLeftButtonDown;                
                b.MouseMove += HandleMove;
                b.MouseLeave += HandleLeave;
                b.MouseLeftButtonUp += HandleLeftButtonUp;
                
                RedrawControl();
            }
        }


        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            
            if (Control != null && Control.Clip == null)
            {
                if (e.PropertyName == NControlView.ClipProperty.PropertyName)
                    // Update clip 
                    if (Element.Clip)
                        Control.Clip = new RectangleGeometry { Rect = new Rect(0, 0, Control.Width, Control.Height) };
                    else
                        Control.Clip = null;
                if (e.PropertyName == NControlView.BackgroundColorProperty.PropertyName ||
                    e.PropertyName == NControlView.HeightProperty.PropertyName ||
                    e.PropertyName == NControlView.WidthProperty.PropertyName)
                    RedrawControl();
            }                        
        }

        #region Drawing

        /// <summary>
        /// Redraws the control by clearing the canvas element and adding new elements
        /// </summary>
        private void RedrawControl()
        {
            if (Element.Width == -1 || Element.Height == -1)
                return;

            _canvas.Children.Clear();
            var canvas = new CanvasCanvas(_canvas);

            Element.Draw(canvas, new NGraphics.Rect(0, 0, Element.Width, Element.Height));
        }

        #endregion

        #region Touch Handlers

        /// <summary>
        /// Touch up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(Control);
            Element.TouchesEnded(new NGraphics.Point[] { 
                new NGraphics.Point(pos.X, pos.Y)
            });
        }

        /// <summary>
        /// Mouse left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLeave(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(Control);
            Element.TouchesCancelled(new NGraphics.Point[] { 
                new NGraphics.Point(pos.X, pos.Y)
            });
        }

        /// <summary>
        /// Moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(Control);
            Element.TouchesMoved(new NGraphics.Point[] { 
                new NGraphics.Point(pos.X, pos.Y)
            });
        }

        /// <summary>
        /// Mouse down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            var pos = e.GetPosition(Control);
            Element.TouchesBegan(new NGraphics.Point[] { 
                new NGraphics.Point(pos.X, pos.Y)
            });
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
            // Invalidate control
            RedrawControl();
        }

        #endregion
	}
}



