using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NControlDemo.FormsApp;
using NControlDemo.Droid.Platform.IoC;
using NControlDemo.Droid.Platform.Mvvm;
using NControlDemo.FormsApp.Mvvm;
using NControl.Plugins.Droid;

namespace NControlDemo.Droid
{
	[Activity (Label = "NControlDemo.Droid", Icon = "@drawable/icon", 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            NControlViewRenderer.Init();

            LoadApplication (new App (new ContainerProvider(), (container) => {

                // Register providers
                container.Register<IImageProvider, ImageProvider>();
            }));
		}
	}
}

