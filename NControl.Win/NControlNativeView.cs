using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NControl.Win
{
    public class NControlNativeView : Grid
    {
        /// <summary>
        /// Canvas element
        /// </summary>
        public Canvas Canvas { get; private set; }

        /// <summary>
        /// Border element
        /// </summary>
        public Border Border { get; private set; }

        public NControlNativeView()
        {
            Canvas = new Canvas();
            Border = new Border { Child = Canvas };
            Children.Add(Border);

            Loaded += (sender, args) => SetClip(IsClipped);
            SizeChanged += (sender, args) => SetClip(IsClipped);
        }

        public bool IsClipped { get; set; }

        public void SetClip(bool clip)
        {
            IsClipped = clip;
            Border.Clip = clip ?
               new RectangleGeometry { Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight) } :
               null;
        }
    }
}
