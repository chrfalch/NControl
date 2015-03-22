/****************************** Module Header ******************************\
Module Name:  BaseContentsView.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using Xamarin.Forms;
using System.Linq.Expressions;
using System.Collections.Generic;
using NControlDemo.Classes;
using NControlDemo.FormsApp.IoC;
using NControlDemo.Helpers;
using NControlDemo.FormsApp.ViewModels;
using NControlDemo.FormsApp.Mvvm;

namespace NControlDemo.FormsApp.Views
{
	/// <summary>
	/// Base view.
	/// </summary>
	public abstract class BaseContentsView<TViewModel>: ContentPage, IView
		where TViewModel : BaseViewModel
	{

		#region Private Members

		/// <summary>
		/// The view decorator.
		/// </summary>
		private readonly ViewDecorator<TViewModel> _viewDecorator;

        /// <summary>
		/// Main relative layout
		/// </summary>
		private readonly RelativeLayout _layout;

        /// <summary>
        /// The property change listeners.
        /// </summary>
        private readonly List<PropertyChangeListener> _propertyChangeListeners = new List<PropertyChangeListener>();

        #endregion

		#region Constructors

		/// <summary>
		/// Default constructor
		/// </summary>
        public BaseContentsView ()
		{
            // Image provider
            ImageProvider = Container.Resolve<IImageProvider>();

			// Set up viewmodel and viewmodel values
            ViewModel = Container.Resolve<TViewModel> ();
			BindingContext = ViewModel;

			// Bind title
			this.SetBinding (Page.TitleProperty, GetPropertyName (vm => vm.Title));

			// Loading/Progress overlay
			_layout = new RelativeLayout ();

			var contents = CreateContents ();

			// Activity overflay and indiciator
			_viewDecorator = new ViewDecorator<TViewModel> (_layout, contents);

			// Set our content to be the relative layout with progress overlays etc.
			Content = _layout;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Returns the ViewModel
		/// </summary>
		public TViewModel ViewModel{get; private set;}

		#endregion

		#region View LifeCycle

		/// <summary>
		/// Raised when the view has appeared on screen.
		/// </summary>
		protected override async void OnAppearing()
		{
            base.OnAppearing();

            await ViewModel.OnAppearingAsync();
			_viewDecorator.OnAppearing ();
		}

        /// <summary>
        /// Raises the disappearing event.
        /// </summary>
		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			ViewModel.OnDisappearingAsync ();
			_viewDecorator.OnDisappearing ();
		}

        /// <summary>
        /// Application developers can override this method to provide behavior when the back button is pressed.
        /// </summary>
        /// <returns>To be added.</returns>
        /// <remarks>To be added.</remarks>
        protected override bool OnBackButtonPressed()
        {
            if (ViewModel.BackButtonCommand != null &&
               ViewModel.BackButtonCommand.CanExecute(null))
                ViewModel.BackButtonCommand.Execute(null);
            
            return true;
        }
			
		#endregion

		#region Protected Members

        /// <summary>
        /// Gets the image provide.
        /// </summary>
        /// <value>The image provide.</value>
        protected IImageProvider ImageProvider {get;private set;}

		/// <summary>
		/// Implement to create the layout on the page
		/// </summary>
		/// <returns>The layout.</returns>
		protected abstract View CreateContents ();

		/// <summary>
		/// Sets the background image.
		/// </summary>
		/// <param name="imageType">Image type.</param>
		protected void SetBackgroundImage(string imageName)
		{
            // Background image
			var image = new Image {
                Source = ImageProvider.GetImageSource(imageName),
				Aspect = Aspect.AspectFill
			};

			_layout.Children.Add (image, Constraint.Constant(0), 
				Constraint.RelativeToParent((parent) => parent.Height - 568), 
				Constraint.RelativeToParent((parent) => parent.Width),
				Constraint.Constant(568));
		}

		/// <summary>
		/// Calls the notify property changed event if it is attached. By using some
		/// Expression/Func magic we get compile time type checking on our property
		/// names by using this method instead of calling the event with a string in code.
		/// </summary>
		/// <param name="property">Property.</param>
		protected string GetPropertyName<TOwner>(Expression<Func<TOwner, object>> property)
		{
			return PropertyNameHelper.GetPropertyName<TOwner> (property);
		}

		/// <summary>
		/// Calls the notify property changed event if it is attached. By using some
		/// Expression/Func magic we get compile time type checking on our property
		/// names by using this method instead of calling the event with a string in code.
		/// </summary>
		/// <param name="property">Property.</param>
		protected string GetPropertyName(Expression<Func<TViewModel, object>> property)
		{
			return PropertyNameHelper.GetPropertyName<TViewModel> (property);
		}

        /// <summary>
        /// Listens for property change.
        /// </summary>
        /// <param name="property">Property.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        protected void ListenForPropertyChange<TObject>(Expression<Func<TObject, object>> property, TObject obj, Action callback)
        {
            var changeListener = new PropertyChangeListener();
            changeListener.Listen<TObject>(property, obj, callback);
            _propertyChangeListeners.Add(changeListener);
        }
		#endregion
	}
}

