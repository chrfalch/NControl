using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.IoC;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Mvvm;
using NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.Mvvm;
using NControl.Plugins.iOS;

namespace NControlDemo.Forms.Xamarin.Plugins.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();
            NControlViewRenderer.Init();

            LoadApplication (new App (new ContainerProvider(), (container) => {

                // Register providers
                container.Register<IRepositoryProvider, RepositoryProvider>();
                container.Register<IImageProvider, ImageProvider>();

            }));

			return base.FinishedLaunching (app, options);
		}
	}
}

