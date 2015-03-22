# NControl
Simple Xamarin.Forms wrapper control around the [NGraphics](https://github.com/praeclarum/NGraphics) library to enable developers to create custom controls without the need for custom renderers. 

The library contains the ```NControlView``` class where real custom cross-platform drawing can be performed without the need for native implementations thanks to the NGraphics library. ```NControlView``` can be used both to do custom drawing and to create complex custom controls.

The library currently supports native renderers for the following platforms:

- Android
- iOS Unified

Example usage:

In your Xamarin.Forms project, add a new NControlView element to your page and provide a drawing function where your custom drawing is performed:

```csharp
public void MyView()
{
  Content = new NControlView {
      DrawingFunction = (canvas, rect) => {
          canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
          canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
      }
  };
}
```
You can also create a subclass of the ```NControlView``` class and override its ```Draw``` function to add your own custom drawing:

```csharp
public class MyControl: NControlView
{
  public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
  {
    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
    canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
  }
}
```
