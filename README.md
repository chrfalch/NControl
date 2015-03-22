# NControl
Simple Xamarin.Forms wrapper control around the [NGraphics](https://github.com/praeclarum/NGraphics) library to enable developers to create custom controls without the need for custom renderers. This library contains one important view - the **NControlView**. This Xamarin.Forms view can be used for direct custom cross-platform drawing or to create complex custom controls.

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
```
There are two ways of drawing in the control. The example above fraws using a drawing function, but you can also subclass the view itself and override its drawing method.
