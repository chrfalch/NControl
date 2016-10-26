# NControl
**NControl** is a Xamarin.Forms wrapper control built around the [NGraphics](https://github.com/praeclarum/NGraphics) library enabling developers to create custom controls without the need for custom renderers. 

The library contains the ```NControlView``` class where real custom cross-platform drawing can be performed without the need for native implementations thanks to the [NGraphics](https://github.com/praeclarum/NGraphics) library. ```NControlView``` can be used both to do custom drawing and to create complex custom controls.

![Screenshot](/../screenshots/Screens/Startup.gif?raw=true) ![Screenshot](/../screenshots/Screens/Startup2.gif?raw=true)

## Supported Platforms
The library currently supports native renderers for the following platforms:

- Android
- iOS Unified
- Windows Phone (8, 8.1 & Silverlight 8.1)
- Windows Store (Windows 8.1)
- UWP (Windows 10)

## Installation
Add the [Nuget](https://www.nuget.org/packages/NControl/) packages to your iOS, Android and Forms-project.

Remember to to add calls to ```NControlViewRenderer.Init()``` after ```Forms.Init()``` in your AppDelegate and in your MainActivity. 

NOTE: there is a special Forms.Init override for UWP where you must include the Assemblies of 3rd party controls and those with custom renderers, like NControl. If you fail to do so, the NControlView won't appear in builds that use the "Compile with .NET Native tool chain" option (Release or App Store builds).
More info from Xamarin [here](https://developer.xamarin.com/guides/xamarin-forms/platform-features/windows/installation/universal/#Troubleshooting)
// For .NET Native compilation, you have to tell Xamarin.Forms which assemblies it should scan for custom controls and renderers
var rendererAssemblies = new[]
{
    typeof(NControl.UWP.NControlViewRenderer).GetTypeInfo().Assembly
};
Xamarin.Forms.Forms.Init(e, rendererAssemblies);

## Example Usage
In your Xamarin.Forms project, add a new NControlView element to your page in the constructor and provide a drawing function where your custom drawing is performed:

```csharp
  Content = new NControlView {
      DrawingFunction = (canvas, rect) => {
          canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, NGraphics.Colors.Red);
          canvas.DrawLine(rect.Width, rect.Top, rect.Left, rect.Height, NGraphics.Colors.Yellow);
      }
  };
```
## Subclassing
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

## Complex Controls
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

## Invalidation
If you need to invalide the control when a property has changed, call the ```Invalidate()``` function to redraw your control:

```csharp
public class MyControl: NControlView
{
  private Xamarin.Forms.Color _foregroundColor = Xamarin.Forms.Color.Red;
  public Xamarin.Forms.Color ForegroundColor { 
    get 
    { 
      return _foregroundColor; 
    }
    set 
    { 
      _foregroundColor = value;
      Invalidate();
    }
  }
  
  public override void Draw(NGraphics.ICanvas canvas, NGraphics.Rect rect)
  {
    canvas.DrawLine(rect.Left, rect.Top, rect.Width, rect.Height, new NGraphics.Color(_foregroundColor.R, _foregroundColor.G,
      _foregroundColor.B, _foregroundColor.A));
  }
}
```
## Touch Events
The ```NControlView``` class also handles touch events - look at the [CircularButtonControl](NControlDemo/NControlDemo.FormsApp/Controls/CircularButtonControl.cs) for an example of how this can be used.

## Demo Controls
The demo solution contains a few different controls based on the ```NControlView``` class:
- [RoundedCornerControl](NControlDemo/NControlDemo.FormsApp/Controls/RoundedBorderControl.cs)
- [CircularButtonControl](NControlDemo/NControlDemo.FormsApp/Controls/CircularButtonControl.cs)
- [ProgressControl](NControlDemo/NControlDemo.FormsApp/Controls/ProgressControl.cs)

The ProgressControl and the CircularButtonControl both internally uses the [FontAwsomeLabel](NControlDemo/NControlDemo.FormsApp/Controls/FontAwesomeLabel.cs) to display glyphs from the [Font Awesome Icon font](http://fortawesome.github.io/Font-Awesome/). 

## Notes
Note that the ProgressControl and the CircularButtonControl contains animations to make the user experience more alive. The demo solution also uses animation to add some eye candy to the demo itself.

