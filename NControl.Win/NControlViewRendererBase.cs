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

using Microsoft.Phone.Controls;
using NControl.Abstractions;
using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using NControl.Win;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;
using Thickness = System.Windows.Thickness;

namespace NControl.Win
{
    /// <summary>
    /// NControlView renderer.
    /// </summary>
    public abstract class NControlViewRendererBase : ViewRenderer<NControlView, Border>
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        protected static void Initialize() 
        {
            Touch.FrameReported += Touch_FrameReported;     
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public NControlViewRendererBase()
            : base()
        {
        }

        /// <summary>
        /// Creates a new canvas 
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        protected abstract NGraphics.ICanvas CreateCanvas(Canvas canvas);

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
                var ctrl = new Border();
                ctrl.Child = new Canvas();
                
                SetNativeControl(ctrl);

                UpdateInputTransparent();

                Control.Loaded += (s, evt) => UpdateClip();
                Control.SizeChanged += (s, evt) => UpdateClip();
            }

            RedrawControl();
        }

        /// <summary>
        /// Redraw when background color changes
        /// </summary>
        protected override void UpdateBackgroundColor()
        {
            // Do not update background!!!! This will destroy clipping. We do 
            // redraw of the background color in the redraw control method
            // base.UpdateBackgroundColor();

            RedrawControl();
        }

        /// <summary>
        /// Raises the element property changed event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control == null)
                return;

            if (e.PropertyName == Layout.IsClippedToBoundsProperty.PropertyName)
                UpdateClip();

            else if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName ||
                e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                RedrawControl();
            else if (e.PropertyName == VisualElement.InputTransparentProperty.PropertyName)
                UpdateInputTransparent();
        }
        
        #region Drawing

        /// <summary>
        /// Redraws the control by clearing the canvas element and adding new elements
        /// </summary>
        private void RedrawControl()
        {
            if (Element.Width.Equals(-1) || Element.Height.Equals(-1))
                return;

            if (Control.Child == null)
                return;

            UpdateClip();

            (Control.Child as Canvas).Children.Clear();
            var canvas = CreateCanvas((Control.Child as Canvas));
            var rect = new NGraphics.Rect(0, 0, Element.Width, Element.Height);
            canvas.DrawRectangle(rect, null, new NGraphics.SolidBrush(
                new NGraphics.Color(Element.BackgroundColor.R,
                Element.BackgroundColor.B, Element.BackgroundColor.G, Element.BackgroundColor.A)));

            Element.Draw(canvas, rect);
        }

        #endregion
       
        #region Private Members

        /// <summary>
        /// Updates clic on the element
        /// </summary>
        private void UpdateClip()
        {
            if (Element.Width.Equals(-1) || Element.Height.Equals(-1) || ActualWidth == 0 || ActualHeight == 0)
                return;

            if (Element.IsClippedToBounds)
                Control.Clip = new RectangleGeometry { Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight) };
            else
                Control.Clip = null;
        }

        /// <summary>
        /// Updates the IsHitTestVisible property on the native control
        /// </summary>
        private void UpdateInputTransparent()
        {
            Control.IsHitTestVisible = !Element.InputTransparent;
        }

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

        #region Static Touch Handler

        /// <summary>
        /// Touch handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected static void Touch_FrameReported(object sender, TouchFrameEventArgs e)
        {
            // Get the primary touch point. We do not track multitouch at the moment.
            var primaryTouchPoint = e.GetPrimaryTouchPoint(System.Windows.Application.Current.RootVisual);

            var uiElements = VisualTreeHelper.FindElementsInHostCoordinates(primaryTouchPoint.Position, System.Windows.Application.Current.RootVisual);
            foreach(var uiElement in uiElements)
            {
                // Are we interested?
                var renderer = uiElement as NControlViewRendererBase;
                if (renderer == null)
                    continue;

                // Get NControlView element
                var element = renderer.Element;
            
                // Get this' position on screen
                var transform = System.Windows.Application.Current.RootVisual.TransformToVisual(renderer.Control);

                // Transform touches
                var touchPoints = e.GetTouchPoints(System.Windows.Application.Current.RootVisual);
                var touches = touchPoints
                    .Select(t => transform.Transform(new System.Windows.Point(t.Position.X, t.Position.Y)))
                    .Select(t => new NGraphics.Point(t.X, t.Y)).ToList();
                
                var result = false;
                if (primaryTouchPoint.Action == TouchAction.Move)
                {
                    result = element.TouchesMoved(touches);
                }
                else if (primaryTouchPoint.Action == TouchAction.Down)
                {
                    result = element.TouchesBegan(touches);
                }
                else if (primaryTouchPoint.Action == TouchAction.Up)
                {
                    result = element.TouchesEnded(touches);
                }

                if (result)
                    break;
            }
        }
        #endregion
    }
}
