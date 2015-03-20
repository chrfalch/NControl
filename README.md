# NControl
Simple Xamarin.Forms wrapper control around the NGraphics library. 

The library currently supports native renderers for the following platforms:

- Android
- iOS Unified

Example usage:

In your Xamarin.Forms project, add a new NControlView element to your page:

```csharp
public void MyView()
{
  Content = new NControlView((ICanvas canvas, Rect rect) => {
    var font = new NGraphics.Font { Family = "Arial", Size = 14 };
    canvas.FillEllipse(rect, NGraphics.Colors.Yellow);
    canvas.DrawLine(new NGraphics.Point(rect.X, rect.Y), new NGraphics.Point(rect.Width, rect.Height), NGraphics.Colors.Red);
    canvas.DrawText("ABCD", new Rect(50, 50, 50, 50), font, NGraphics.TextAlignment.Left, null, Brushes.Black);
  }){ 
    WidthRequest = 100, 
    HeightRequest = 100, 
    BackgroundColor = global::Xamarin.Forms.Color.Transparent
  };
}
