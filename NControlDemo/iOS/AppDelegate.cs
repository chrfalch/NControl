using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using NControlDemo.iOS.Platform.IoC;
using NControlDemo.FormsApp;
using NControlDemo.FormsApp.Mvvm;
using NControlDemo.iOS.Platform.Mvvm;
using NControl.Plugins.iOS;

namespace NControlDemo.iOS
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
                container.Register<IImageProvider, ImageProvider>();

            }));

			return base.FinishedLaunching (app, options);
		}
	}
}

