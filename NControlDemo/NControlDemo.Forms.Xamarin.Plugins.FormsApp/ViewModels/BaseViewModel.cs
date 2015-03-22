/****************************** Module Header ******************************\
Module Name:  BaseViewModel.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq.Expressions;
using NControlDemo.Classes;
using NControlDemo.FormsApp.Providers;
using NControlDemo.Helpers;

namespace NControlDemo.FormsApp.ViewModels
{
	/// <summary>
	/// Base view model.
	/// </summary>
	public abstract class BaseViewModel : BaseNotifyPropertyChangedObject
	{
		#region Private Members

		/// <summary>
		/// The busy count.
		/// </summary>
		private int _busyCount = 0;

		/// <summary>
		/// The view model storage.
		/// </summary>
		private readonly ViewModelStorageProvider _viewModelStorage = 
			new ViewModelStorageProvider();

		/// <summary>
		/// Command dependencies - key == property, value = list of commands
		/// </summary>
		private readonly Dictionary<string, List<Command>> _commandDependencies = 
			new Dictionary<string, List<Command>>();

		/// <summary>
		/// Command dependencies - key == property, value = list of property names
		/// </summary>
		private readonly Dictionary<string, List<Expression<Func<object>>>> _propertyDependencies = 
			new Dictionary<string, List<Expression<Func<object>>>>();

		/// <summary>
		/// Command cache
		/// </summary>
		private readonly Dictionary<string, Command> _commands = new Dictionary<string, Command> ();

        /// <summary>
        /// The notify change for same values.
        /// </summary>
        private readonly List<string> _notifyChangeForSameValues = new List<string>();

        /// <summary>
        /// The property change listeners.
        /// </summary>
        private readonly List<PropertyChangeListener> _propertyChangeListeners = new List<PropertyChangeListener>();

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CF.Xamarin.Forms.Mvvm.ViewModels.BaseViewModel"/> class.
		/// </summary>
		/// <param name="viewModelStorage">View model storage.</param>
		public BaseViewModel()
		{
			// Property dependencies
			AddPropertyDependency<BaseViewModel> (() => IsBusyText, () => IsBusyTextVisible);
            		
		}

		#endregion

		#region Protected Members

		/// <summary>
		/// Sets a value in viewmodel storage and raises property changed if value has changed
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="value">Value.</param>
		/// <typeparam name="TValueType">The 1st type parameter.</typeparam>
		protected bool SetValue<TValueType>(Expression<Func<object>> property, TValueType value) 
		{
			var existingValue = GetValue<TValueType>(property);
            var propertyName = PropertyNameHelper.GetPropertyName<BaseViewModel>(property);

			// Check for equality
            if (!_notifyChangeForSameValues.Contains(propertyName) && 
                EqualityComparer<TValueType>.Default.Equals (existingValue, value))
				return false;

			_viewModelStorage.SetObjectForKey<TValueType> (propertyName, value);

			RaisePropertyChangedEvent (property);

			// Dependent properties?
			if (_propertyDependencies.ContainsKey (propertyName)) {
				foreach (var dependentProperty in _propertyDependencies[propertyName])
					RaisePropertyChangedEvent (dependentProperty);
			}

			// Dependent commands
			if (_commandDependencies.ContainsKey (propertyName)) {
				foreach (var dependentCommand in _commandDependencies[propertyName])
					RaiseCommandStateChangedEvent (dependentCommand);
			}

			return true;
		}

		/// <summary>
		/// Returns a value from the viewmodel storage
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="name">Name.</param>
		/// <typeparam name="TValueType">The 1st type parameter.</typeparam>
		protected TValueType GetValue<TValueType>(Expression<Func<object>> property) 
		{
			return GetValue<TValueType> (property, default(TValueType));
		}

		/// Returns a value from the viewmodel storage
		/// </summary>
		/// <returns>The value.</returns>
		/// <param name="name">Name.</param>
		/// <typeparam name="TValueType">The 1st type parameter.</typeparam>
		protected TValueType GetValue<TValueType>(Expression<Func<object>> property, TValueType defaultValue) 
		{
			var propertyName = PropertyNameHelper.GetPropertyName<BaseViewModel> (property);
			return _viewModelStorage.GetObjectForKey<TValueType> (propertyName, defaultValue);
		}

		/// <summary>
		/// Adds a dependency between a command and a property. Whenever the property changes, the command's 
		/// state will be updated
		/// </summary>
		/// <param name="property">Source Property.</param>
		/// <param name="command">Target Command.</param>
		protected void AddCommandDependency<TViewModel>(Expression<Func<object>> property, Command command)
		{
			var propertyName = PropertyNameHelper.GetPropertyName <TViewModel>(property);
			if (!_commandDependencies.ContainsKey (propertyName))
				_commandDependencies.Add (propertyName, new List<Command> ());

			var list = _commandDependencies [propertyName];
			list.Add (command);
		}

		/// <summary>
		/// Adds a dependency between a property and another property. Whenever the property changes, the command's 
		/// state will be updated
		/// </summary>
		/// <param name="property">Source property.</param>
		/// <param name="dependantProperty">Target property.</param>
		protected void AddPropertyDependency<TViewModel>(Expression<Func<object>> property, 
			Expression<Func<object>> dependantProperty)
		{
			var propertyName = PropertyNameHelper.GetPropertyName <TViewModel>(property);
			if (!_propertyDependencies.ContainsKey (propertyName))
				_propertyDependencies.Add (propertyName, new List<Expression<Func<object>>> ());

			var list = _propertyDependencies [propertyName];
			list.Add (dependantProperty);
		}

        /// <summary>
        /// Adds the raise notify changed for property when value is the same.
        /// </summary>
        /// <param name="property">Property.</param>
        /// <param name="dependantProperty">Dependant property.</param>
        /// <typeparam name="TViewModel">The 1st type parameter.</typeparam>
        protected void AddRaiseNotifyChangedForPropertyWhenValueIsTheSame<TViewModel>(Expression<Func<object>> property)
        {
            var propertyName = PropertyNameHelper.GetPropertyName <TViewModel>(property);
            _notifyChangeForSameValues.Add(propertyName);
        }

		/// <summary>
		/// Raises the command state changed event.
		/// </summary>
		/// <param name="command">Command.</param>
		protected void RaiseCommandStateChangedEvent(Command command)
		{
			command.ChangeCanExecute ();
		}

		/// <summary>
		/// Creates or returns the 
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="action">Action.</param>
		/// <param name="state">State.</param>
		protected Command GetOrCreateCommand<TViewModel>(Expression<Func<object>> commandProperty,
			Command command)
		{
			var commandName = PropertyNameHelper.GetPropertyName<TViewModel> (commandProperty);
			if (!_commands.ContainsKey (commandName))
				_commands.Add (commandName, command);

			return _commands [commandName];
		}

		/// <summary>
		/// Creates or returns the 
		/// </summary>
		/// <returns>The command.</returns>
		/// <param name="action">Action.</param>
		/// <param name="state">State.</param>
		protected Command<T> GetOrCreateCommand<TViewModel, T>(Expression<Func<object>> commandProperty,
			Command<T> command)
		{
			var commandName = PropertyNameHelper.GetPropertyName<TViewModel> (commandProperty);
			if (!_commands.ContainsKey (commandName))
				_commands.Add (commandName, command);

			return _commands [commandName] as Command<T>;
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

		#region ViewModel LifeCycle

		/// <summary>
		/// Initializes the viewmodel
		/// </summary>
		public virtual Task InitializeAsync()
		{
			return Task.FromResult (true);
		}

		/// <summary>
		/// Override to implement logic when the view has been set up on screen
		/// </summary>
		public virtual async Task OnAppearingAsync()
		{
		   // Call initialize
            await InitializeAsync ();
            		
		}

		/// <summary>
		/// Called whenever the view is hidden
		/// </summary>
		public virtual Task OnDisappearingAsync()
		{		
			return Task.FromResult (true);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Returns the view title
		/// </summary>
		/// <value>The view title.</value>
		public virtual string Title { get { return string.Empty; } }

        /// <summary>
        /// Gets the back button command.
        /// </summary>
        /// <value>The back button command.</value>
        public virtual Command BackButtonCommand { get { return null; } }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public bool IsBusy { 
			get{ 
				return _busyCount > 0;
			}
			set{ 
				if (value)
					_busyCount=1;
				else
					_busyCount=0;

				RaisePropertyChangedEvent (() => IsBusy);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public string IsBusyText 
		{ 
			get{ return GetValue<string>(() => IsBusyText); }
			set{ SetValue<string> (() => IsBusyText, value); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is busy.
		/// </summary>
		/// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
		public bool IsBusyTextVisible { 
			get { return !String.IsNullOrEmpty (IsBusyText); }
		}

		#endregion

	}
}

