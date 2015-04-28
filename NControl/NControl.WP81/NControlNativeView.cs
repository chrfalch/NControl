using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NControl.WP81
{
    public class NControlNativeView : Grid
    {
        public NControlNativeView()
        {
            Loaded += (sender, args) => SetClip(IsClipped);
            SizeChanged += (sender, args) => SetClip(IsClipped);
        }

        public bool IsClipped { get; set; }

        public void SetClip(bool clip)
        {
            IsClipped = clip;
            Clip = clip ? 
                new RectangleGeometry { Rect = new Rect(0, 0, this.ActualWidth, this.ActualHeight) } : 
                null;
        }
    }
}
