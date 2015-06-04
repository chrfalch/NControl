using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NGraphics;
using Transform = NGraphics.Transform;

namespace NControl.WP80
{
    /// <summary>
    /// Windows Phone Silverlight Platform
    /// </summary>
    public class PhoneSilverlightPlatform : IPlatform
    {
        /// <summary>
        /// Returns the name of the platform
        /// </summary>
        public string Name { get { return "PhoneSilverlight WP80"; } }

        /// <summary>
        /// Creates an object implementing the IImageCanvas interface
        /// </summary>
        /// <param name="size"></param>
        /// <param name="scale"></param>
        /// <param name="transparency"></param>
        /// <returns></returns>
        public IImageCanvas CreateImageCanvas(Size size, double scale = 1.0, bool transparency = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates an object implementing the IImage interface with the given paramters
        /// </summary>
        /// <param name="colors"></param>
        /// <param name="width"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public IImage CreateImage(NGraphics.Color[] colors, int width, double scale = 1.0)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Loads an IImage from stream
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public IImage LoadImage(Stream stream)
        {
            var bitmap = new BitmapImage();
            bitmap.SetSource(stream);
            return new BitmapImageImage(bitmap);
        }

        /// <summary>
        /// Loads an IImage from a path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IImage LoadImage(string path)
        {
            using (var fs = File.OpenRead(path))
            {
                var bitmap = new BitmapImage();
                bitmap.SetSource(fs);
                return new BitmapImageImage(bitmap);
            }
        }
    }

    /// <summary>
    /// Bitmap encapsulation
    /// </summary>
    public class BitmapImageImage : IImage
    {
        /// <summary>
        /// Bitmap
        /// </summary>
        public BitmapImage Bitmap { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public BitmapImageImage(BitmapImage bitmap)
        {
            Bitmap = bitmap;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public void SaveAsPng(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        public void SaveAsPng(Stream stream)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Creates a new PhoneSilverlightImageSurface
    /// </summary>
    public class CanvasCanvas : IImageCanvas
    {
        #region Private Members

        /// <summary>
        /// Wrapped Canvas
        /// </summary>
        private readonly Canvas _canvas;

        /// <summary>
        /// Saved states
        /// </summary>
        private readonly Stack<Transform> _savedStates = new Stack<Transform>();

        private Transform CurrentTransform
        {
            get
            {
                return _savedStates.Count > 0 ? _savedStates.Peek() : NGraphics.Transform.Identity;
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="canvas"></param>
        public CanvasCanvas(Canvas canvas)
        {
            _canvas = canvas;
        }

        /// <summary>
        /// Returns the canvas rendered on an IImage
        /// </summary>
        /// <returns></returns>
        public IImage GetImage()
        {
            throw new NotImplementedException();
        }

        #region Properties

        /// <summary>
        /// Returns the size of the canvase
        /// </summary>
        public Size Size { get { return new Size(_canvas.Width, _canvas.Height); } }

        /// <summary>
        /// Returns scale, always 1.0
        /// </summary>
        public double Scale { get { return 1.0; } }

        #endregion

        #region Drawing Members

        /// <summary>
        /// Saves the state of the drawing operations
        /// </summary>
        public void SaveState()
        {
            _savedStates.Push(CurrentTransform);
        }

        /// <summary>
        /// Transforms
        /// </summary>
        /// <param name="transform"></param>
        public void Transform(NGraphics.Transform transform)
        {
            // Remove current state (if any). It is replaced by the new state.
            if(_savedStates.Count > 0)
            {
                _savedStates.Pop();
            }

            _savedStates.Push(transform);
        }

        /// <summary>
        /// Restores the state
        /// </summary>
        public void RestoreState()
        {
            _savedStates.Pop();
        }

        /// <summary>
        /// Draws text
        /// </summary>
        /// <param name="text"></param>
        /// <param name="frame"></param>
        /// <param name="font"></param>
        /// <param name="alignment"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public void DrawText(string text, Rect frame, Font font, TextAlignment alignment = TextAlignment.Left,
            Pen pen = null, NGraphics.Brush brush = null)
        {
            var textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Width = frame.Width;
            textBlock.Height = frame.Height;
            Canvas.SetLeft(textBlock, frame.X);
            Canvas.SetTop(textBlock, frame.Y);

            textBlock.FontFamily = new FontFamily(font.Family);
            textBlock.FontSize = font.Size;

            switch (alignment)
            {
                case TextAlignment.Left:
                    textBlock.TextAlignment = System.Windows.TextAlignment.Left;
                    break;
                case TextAlignment.Center:
                    textBlock.TextAlignment = System.Windows.TextAlignment.Center;
                    break;
                case TextAlignment.Right:
                    textBlock.TextAlignment = System.Windows.TextAlignment.Right;
                    break;
            }

            if (pen != null)
                textBlock.Foreground = new SolidColorBrush(new System.Windows.Media.Color
                {
                    A = pen.Color.A,
                    R = pen.Color.R,
                    G = pen.Color.G,
                    B = pen.Color.B
                });

            textBlock.RenderTransform = Conversions.GetTransform(CurrentTransform);
            _canvas.Children.Add(textBlock);
        }

        /// <summary>
        /// Draws a path
        /// </summary>
        /// <param name="ops"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public void DrawPath(IEnumerable<PathOp> ops, Pen pen = null, NGraphics.Brush brush = null)
        {
            if (pen == null && brush == null)
                return;

            var pathEl = new System.Windows.Shapes.Path();

            if (brush != null)
                pathEl.Fill = GetBrush(brush);

            if (pen != null)
            {
                pathEl.Stroke = GetStroke(pen);
                pathEl.StrokeThickness = pen.Width;
            }

            var geo = new StringBuilder();

            foreach (var op in ops)
            {
                var mt = op as MoveTo;
                if (mt != null)
                {
                    geo.AppendFormat(CultureInfo.InvariantCulture, " M {0},{1}", mt.Point.X, mt.Point.Y);
                    continue;
                }

                var lt = op as LineTo;
                if (lt != null)
                {
                    geo.AppendFormat(CultureInfo.InvariantCulture, " L {0},{1}", lt.Point.X, lt.Point.Y);
                    continue;
                }

                var at = op as ArcTo;
                if (at != null)
                {
                    var p = at.Point;
                    var r = at.Radius;

                    geo.AppendFormat(CultureInfo.InvariantCulture, " A {0},{1} 0 {2} {3} {4},{5}",
                        r.Width, r.Height,
                        at.LargeArc ? 1 : 0,
                        at.SweepClockwise ? 1 : 0,
                        p.X, p.Y);
                    continue;
                }

                var ct = op as CurveTo;
                if (ct != null)
                {
                    var p = ct.Point;
                    var c1 = ct.Control1;
                    var c2 = ct.Control2;
                    geo.AppendFormat(CultureInfo.InvariantCulture, " C {0},{1} {2},{3} {4},{5}",
                        c1.X, c1.Y, c2.X, c2.Y, p.X, p.Y);
                    continue;
                }

                var cp = op as ClosePath;
                if (cp != null)
                {
                    geo.Append(" z");
                    continue;
                }
            }

            // Convert path string to geometry
            var b = new Binding { Source = geo.ToString() };
            BindingOperations.SetBinding(pathEl, System.Windows.Shapes.Path.DataProperty, b);

            pathEl.RenderTransform = Conversions.GetTransform(CurrentTransform);
            _canvas.Children.Add(pathEl);
        }

        /// <summary>
        /// Draws a rectangle
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>

        public void DrawRectangle(Rect frame, Pen pen = null, NGraphics.Brush brush = null)
        {
            var rectangleEl = new System.Windows.Shapes.Rectangle();
            var offset = pen != null ? pen.Width : 0.0;
            rectangleEl.Width = frame.Width + offset;
            rectangleEl.Height = frame.Height + offset;

            if (brush != null)
                rectangleEl.Fill = GetBrush(brush);

            if (pen != null)
            {
                rectangleEl.Stroke = GetStroke(pen);
                rectangleEl.StrokeThickness = pen.Width;
            }

            rectangleEl.RenderTransform = Conversions.GetTransform(CurrentTransform);
            _canvas.Children.Add(rectangleEl);
            Canvas.SetLeft(rectangleEl, frame.X - offset / 2.0);
            Canvas.SetTop(rectangleEl, frame.Y - offset / 2.0);
        }

        /// <summary>
        /// Draws an ellipse
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="pen"></param>
        /// <param name="brush"></param>
        public void DrawEllipse(Rect frame, Pen pen = null, NGraphics.Brush brush = null)
        {
            var ellipseEl = new System.Windows.Shapes.Ellipse();
            var offset = pen != null ? pen.Width : 0.0;

            ellipseEl.Width = frame.Width + offset;
            ellipseEl.Height = frame.Height + offset;

            if (brush != null)
                ellipseEl.Fill = GetBrush(brush);

            if (pen != null)
            {
                ellipseEl.Stroke = GetStroke(pen);
                ellipseEl.StrokeThickness = pen.Width;
            }

            ellipseEl.RenderTransform = Conversions.GetTransform(CurrentTransform);
            _canvas.Children.Add(ellipseEl);
            Canvas.SetLeft(ellipseEl, frame.X - offset / 2.0);
            Canvas.SetTop(ellipseEl, frame.Y - offset / 2.0);
        }

        /// <summary>
        /// Draws an image
        /// </summary>
        /// <param name="image"></param>
        /// <param name="frame"></param>
        /// <param name="alpha"></param>
        public void DrawImage(IImage image, Rect frame, double alpha = 1.0)
        {
            var ii = image as BitmapImageImage;
            if (ii != null)
            {
                var imageEl = new Image();
                imageEl.Source = ii.Bitmap;
                imageEl.Width = frame.Width;
                imageEl.Height = frame.Height;

                imageEl.RenderTransform = Conversions.GetTransform(CurrentTransform);
                _canvas.Children.Add(imageEl);
                Canvas.SetLeft(imageEl, frame.X);
                Canvas.SetTop(imageEl, frame.Y);
            }
        }

        #endregion

        #region Brushes

        /// <summary>
        /// Returns a Windows brush from the NGraphics brush
        /// </summary>
        /// <param name="fromBrush"></param>
        /// <returns></returns>
        private System.Windows.Media.Brush GetBrush(NGraphics.Brush fromBrush)
        {
            var sb = fromBrush as SolidBrush;
            if (sb != null)
            {
                // Solid brush
                return new SolidColorBrush(new System.Windows.Media.Color
                {
                    A = sb.Color.A,
                    R = sb.Color.R,
                    G = sb.Color.G,
                    B = sb.Color.B
                });
            }

            var lb = fromBrush as NGraphics.LinearGradientBrush;
            if (lb != null)
            {
                // Linear gradient
                var gradStops = new GradientStopCollection();
                var n = lb.Stops.Count;
                if (n >= 2)
                {
                    var locs = new float[n];
                    var comps = new int[n];
                    for (var i = 0; i < n; i++)
                    {
                        var s = lb.Stops[i];
                        gradStops.Add(new System.Windows.Media.GradientStop
                        {
                            Color = new System.Windows.Media.Color
                            {
                                A = s.Color.A,
                                R = s.Color.R,
                                B = s.Color.B,
                                G = s.Color.G,
                            },
                            Offset = s.Offset,
                        });
                    }
                }

                var grad = new System.Windows.Media.LinearGradientBrush(gradStops, 90);
                return grad;
            }

            var rb = fromBrush as NGraphics.RadialGradientBrush;
            if (rb != null)
            {
                // Radial gradient
                var grad = new System.Windows.Media.RadialGradientBrush();
                var n = rb.Stops.Count;
                if (n >= 2)
                {
                    var locs = new float[n];
                    var comps = new int[n];
                    for (var i = 0; i < n; i++)
                    {
                        var s = rb.Stops[i];
                        grad.GradientStops.Add(new System.Windows.Media.GradientStop
                        {
                            Color = new System.Windows.Media.Color
                            {
                                A = s.Color.A,
                                R = s.Color.R,
                                B = s.Color.B,
                                G = s.Color.G,
                            },
                            Offset = s.Offset,
                        });
                    }
                }
                return grad;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fromBrush"></param>
        /// <returns></returns>
        private System.Windows.Media.Brush GetStroke(NGraphics.Pen fromPen)
        {
            return new SolidColorBrush(new System.Windows.Media.Color
            {
                A = fromPen.Color.A,
                R = fromPen.Color.R,
                G = fromPen.Color.G,
                B = fromPen.Color.B
            });
        }

        #endregion
    }

    public class Conversions
    {
        public static System.Windows.Media.Transform GetTransform(Transform trans)
        {
            return new MatrixTransform()
            {
                Matrix = new Matrix(trans.A, trans.B, trans.C, trans.D, trans.E, trans.F)
            };
        }
    }
}

