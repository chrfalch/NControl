using System;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp;
using System.Linq;

using Foundation;
using UIKit;

namespace NControlDemo.Forms.Xamarin.Plugins.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}

