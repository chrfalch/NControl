/************************************************************************
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2025 - Christian Falch
 * 
 * Permission is hereby granted, free of charge, to any person obtaining 
 * a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including 
 * without limitation the rights to use, copy, modify, merge, publish, 
 * distribute, sublicense, and/or sell copies of the Software, and to 
 * permit persons to whom the Software is furnished to do so, subject 
 * to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be 
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 ************************************************************************/

using System;
using Xamarin.Forms;
using NControlDemo.Helpers;
using NControlDemo.FormsApp.Controls;
using NControlDemo.FormsApp.ViewModels;

namespace NControlDemo.FormsApp.Views
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

