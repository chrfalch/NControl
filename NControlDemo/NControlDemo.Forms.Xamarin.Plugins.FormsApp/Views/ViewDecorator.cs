/****************************** Module Header ******************************\
Module Name:  ViewDecorator.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins.Helpers;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Controls;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.Views
{
	/// <summary>
	/// Decorator for views of different base type
	/// </summary>
	public class ViewDecorator<TViewModel>: object
		where TViewModel : BaseViewModel 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ViewDecorator"/> class.
		/// </summary>
		/// <param name="relativeLayout">Relative layout.</param>
		/// <param name="contents">Contents.</param>
		public ViewDecorator (RelativeLayout relativeLayout, View contents)
		{
			// Add contents
			relativeLayout.Children.Add ( contents, () => relativeLayout.Bounds);

			// Create overlay
			var activityIndicatorOverlay = new RelativeLayout {
				BackgroundColor = Color.FromRgba (0x05, 0x05, 0x05, 0.75)
			};

			activityIndicatorOverlay.SetBinding(RelativeLayout.IsVisibleProperty, 
				PropertyNameHelper.GetPropertyName<TViewModel>((vm) => vm.IsBusy));

			relativeLayout.Children.Add ( activityIndicatorOverlay, () => relativeLayout.Bounds);

			// Create darker rectangle in the middle
            var activityIndicatorBackground = new RoundedBorderControl {
				BackgroundColor = Color.FromRgba(0, 0, 0, 0.78),
                CornerRadius = 8,
			};

			activityIndicatorOverlay.Children.Add (activityIndicatorBackground, () => 
				new Rectangle (activityIndicatorOverlay.Width / 2 - 81, activityIndicatorOverlay.Height / 2 - 41, 81*2, 82));

			// Create indicator
			var activityIndicator = new ActivityIndicator {Color = Color.White};
			activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, 
				PropertyNameHelper.GetPropertyName<TViewModel>((vm) => vm.IsBusy));

			activityIndicatorOverlay.Children.Add (activityIndicator, () =>  
				new Rectangle (activityIndicatorOverlay.Width / 2 - 16, activityIndicatorOverlay.Height / 2 - 24, 32, 32));

			// Create label with progress text
			var activityLabel = new Label {
				TextColor = Color.White,
				BackgroundColor = Color.Transparent,
				FontSize = 12,
				XAlign = TextAlignment.Center
			};

			activityLabel.SetBinding(Label.TextProperty,
				PropertyNameHelper.GetPropertyName<TViewModel>((vm) => vm.IsBusyText));

			activityLabel.SetBinding(Label.IsVisibleProperty,
				PropertyNameHelper.GetPropertyName<TViewModel>((vm) => vm.IsBusyTextVisible));

			activityIndicatorOverlay.Children.Add ( activityLabel, () => 
				new Rectangle (activityIndicatorOverlay.X, activityIndicatorOverlay.Height / 2 + 14, 
                    activityIndicatorOverlay.Width, 32));

		}

		/// <summary>
		/// Called when appearing
		/// </summary>
		public void OnAppearing()
		{

		}

		/// <summary>
		/// Raises the disappearing event.
		/// </summary>
		public void OnDisappearing()
		{
		}
	}
}

