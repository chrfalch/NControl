using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.IoC;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Droid.Platform.Mvvm;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Mvvm;
using NControl.Plugins.Droid;

namespace NControlDemo.Forms.Xamarin.Plugins.Droid
{
	[Activity (Label = "NControlDemo.Forms.Xamarin.Plugins.Droid", Icon = "@drawable/icon", 
        MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);
            NControlViewRenderer.Init();

            LoadApplication (new App (new ContainerProvider(), (container) => {

                // Register providers
                container.Register<IRepositoryProvider, RepositoryProvider>();
                container.Register<IImageProvider, ImageProvider>();
            }));
		}
	}
}

