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
using System.Linq;
using Windows.Devices.Input;
using NControl.Abstractions;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using NControl.WindowsStore;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.WindowsStore
{
    /// <summary>
    /// NControlView renderer.
    /// </summary>
    public class NControlViewRenderer : ViewRenderer<NControlView, NControlNativeView>
    {
        /// <summary>
        /// Canvas element
        /// </summary>
        protected Canvas Canvas;

        /// <summary>
        /// Border element
        /// </summary>
        protected Border Border;

        /// <summary>
        /// Constructor
        /// </summary>
        public NControlViewRenderer()
            : base()
        {
            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;
        }

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
                var ctrl = new NControlNativeView();
                Canvas = new Canvas();
                Border = new Border
                {
                    Child = Canvas,                    
                };                
                
                ctrl.Children.Add(Border);

                SetNativeControl(ctrl);

                UpdateClip();
                UpdateInputTransparent();
            }

            RedrawControl();
        }

        /// <summary>
        /// Redraw when background color changes
        /// </summary>
        protected override void UpdateBackgroundColor()
        {
            base.UpdateBackgroundColor();

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
                e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                // Redraw when height/width changes
                UpdateClip();
                RedrawControl();
            }
            else if (e.PropertyName == NControlView.BackgroundColorProperty.PropertyName)
                RedrawControl();
            else if (e.PropertyName == NControlView.InputTransparentProperty.PropertyName)
                UpdateInputTransparent();
        }

        private void OnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            // Get the primary touch point. We do not track multitouch at the moment.
            var primaryTouchPoint = pointerRoutedEventArgs.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content);

            var uiElements = VisualTreeHelper.FindElementsInHostCoordinates(primaryTouchPoint.Position,
                Windows.UI.Xaml.Window.Current.Content);
            foreach (var uiElement in uiElements)
            {
                // Are we interested?
                var renderer = uiElement as NControlViewRenderer;
                if (renderer == null)
                    continue;

                // Get NControlView element
                var element = renderer.Element;

                // Get this' position on screen
                var transform = Windows.UI.Xaml.Window.Current.Content.TransformToVisual(renderer.Control);

                // Transform touches
                var touchPoints = pointerRoutedEventArgs.GetIntermediatePoints(Windows.UI.Xaml.Window.Current.Content);
                var touches = touchPoints
                    .Select(t => transform.TransformPoint(new Windows.Foundation.Point(t.Position.X, t.Position.Y)))
                    .Select(t => new NGraphics.Point(t.X, t.Y)).ToList();

                if (element.TouchesBegan(touches))
                    break;
            }
        }

        private void OnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            // Get the primary touch point. We do not track multitouch at the moment.
            var primaryTouchPoint = pointerRoutedEventArgs.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content);

            var uiElements = VisualTreeHelper.FindElementsInHostCoordinates(primaryTouchPoint.Position,
                Windows.UI.Xaml.Window.Current.Content);
            foreach (var uiElement in uiElements)
            {
                // Are we interested?
                var renderer = uiElement as NControlViewRenderer;
                if (renderer == null)
                    continue;

                // Get NControlView element
                var element = renderer.Element;

                // Get this' position on screen
                var transform = Windows.UI.Xaml.Window.Current.Content.TransformToVisual(renderer.Control);

                // Transform touches
                var touchPoints = pointerRoutedEventArgs.GetIntermediatePoints(Windows.UI.Xaml.Window.Current.Content);
                var touches = touchPoints
                    .Select(t => transform.TransformPoint(new Windows.Foundation.Point(t.Position.X, t.Position.Y)))
                    .Select(t => new NGraphics.Point(t.X, t.Y)).ToList();

                if (element.TouchesMoved(touches))
                    break;
            }
        }

        private void OnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            // Get the primary touch point. We do not track multitouch at the moment.
            var primaryTouchPoint = pointerRoutedEventArgs.GetCurrentPoint(Windows.UI.Xaml.Window.Current.Content);

            var uiElements = VisualTreeHelper.FindElementsInHostCoordinates(primaryTouchPoint.Position,
                Windows.UI.Xaml.Window.Current.Content);
            foreach (var uiElement in uiElements)
            {
                // Are we interested?
                var renderer = uiElement as NControlViewRenderer;
                if (renderer == null)
                    continue;

                // Get NControlView element
                var element = renderer.Element;

                // Get this' position on screen
                var transform = Windows.UI.Xaml.Window.Current.Content.TransformToVisual(renderer.Control);

                // Transform touches
                var touchPoints = pointerRoutedEventArgs.GetIntermediatePoints(Windows.UI.Xaml.Window.Current.Content);
                var touches = touchPoints
                    .Select(t => transform.TransformPoint(new Windows.Foundation.Point(t.Position.X, t.Position.Y)))
                    .Select(t => new NGraphics.Point(t.X, t.Y)).ToList();

                if (element.TouchesEnded(touches))
                    break;
            }
        }
                
        #region Drawing

        /// <summary>
        /// Redraws the control by clearing the canvas element and adding new elements
        /// </summary>
        private void RedrawControl()
        {
            if (Element.Width.Equals(-1) || Element.Height.Equals(-1))
                return;

            if (Canvas == null)
                return;

            Canvas.Children.Clear();
            var canvas = new CanvasCanvas(Canvas);

            Element.Draw(canvas, new NGraphics.Rect(0, 0, Element.Width, Element.Height));
        }

        #endregion
       
        #region Private Members

        /// <summary>
        /// Updates clic on the element
        /// </summary>
        private void UpdateClip()
        {
            if (Element.Width.Equals(-1) || Element.Height.Equals(-1))
                return;

            Control.SetClip(Element.IsClippedToBounds);
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
    }
}
