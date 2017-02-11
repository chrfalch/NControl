using NControl.Abstractions;
using NControl.UWP;
using NGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(NControlView), typeof(NControlViewRenderer))]
namespace NControl.UWP
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
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
        }

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

            // handle the situation when OnElementChanged is called while the view is going to be removed
            if ((e.OldElement != null) && (e.NewElement == null) && (Control == null))
                return;


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
