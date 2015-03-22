# NControl
**NControl** is a Xamarin.Forms wrapper control around the [NGraphics](https://github.com/praeclarum/NGraphics) library enabling developers to create custom controls without the need for custom renderers. 

The library contains the ```NControlView``` class where real custom cross-platform drawing can be performed without the need for native implementations thanks to the [NGraphics](https://github.com/praeclarum/NGraphics) library. ```NControlView``` can be used both to do custom drawing and to create complex custom controls.

**Supported Platforms**

The library currently supports native renderers for the following platforms:

- Android
- iOS Unified

**Example Usage:**

In your Xamarin.Forms project, add a new NControlView element to your page in the constructor and provide a drawing function where your custom drawing is performed:

```csharp
  Content = new NControlView {
      DrawingFunction = (canvas, rect) => {
          canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
          canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
      }
  };
```
**Subclassing:**

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

**Complex controls:**

```NControlView``` supports containing other controls since it inherits from the ```ContentView``` class. Building composite controls is as simple as setting the Content property of your subclassed control:

```csharp
public class MyControl: NControlView
{
  public MyControl()
  {
    Content = new Label {BackgroundColor = Xamarin.Forms.Color.Transparent};
  }
  
  public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
  {
    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
    canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
  }
}
```
Now your custom control will have a label that can draw text in the control. Remember that you can set the Content property to point to anything as long as it is a VisualElement. This means that you can add layouts containing other controls as well as a single control. 

**Invalidate:**

If you need to invalide the control when a property has changed, call the ```Invalidate()``` function to redraw your control:

```csharp
public class MyControl: NControlView
{
  public MyControl()
  {
    Content = new Label {BackgroundColor = Xamarin.Forms.Color.Transparent};
  }
  
  public string Text { 
    get 
    { 
      return (Content as Label).Text; 
    }
    set 
    { 
      (Content as Label).Text = value;
      Invalidate();
    }
  }
  
  public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
  {
    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
    canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
  }
}
```
**Touch events**

The ```NControlView``` class also handles touch events - look at the [CircularButtonControl](https://github.com/chrfalch/NControl/blob/master/NControlDemo/NControlDemo.Forms.Xamarin.Plugins.FormsApp/Controls/CircularButtonControl.cs) for an example of how this can be used.

**Demo controls**

The demo solution contains a few different controls based on the ```NControlView``` class:
- [RoundedCornerControl](https://github.com/chrfalch/NControl/blob/master/NControlDemo/NControlDemo.Forms.Xamarin.Plugins.FormsApp/Controls/RoundedBorderControl.cs)
- [CircularButtonControl](https://github.com/chrfalch/NControl/blob/master/NControlDemo/NControlDemo.Forms.Xamarin.Plugins.FormsApp/Controls/CircularButtonControl.cs)
- [ProgressControl](https://github.com/chrfalch/NControl/blob/master/NControlDemo/NControlDemo.Forms.Xamarin.Plugins.FormsApp/Controls/ProgressControl.cs)

The ProgressControl and the CircularButtonControl both internally uses the [FontAwsomeLabel](https://github.com/chrfalch/NControl/blob/master/NControlDemo/NControlDemo.Forms.Xamarin.Plugins.FormsApp/Controls/FontAwesomeLabel.cs) to display glyphs from the [Font Awesome Icon font](http://fortawesome.github.io/Font-Awesome/). 

**Notes**

Note that the ProgressControl and the CircularButtonControl contains animations to make the user experience more alive. The demo solution also uses animation to add some eye candy to the demo itself.

