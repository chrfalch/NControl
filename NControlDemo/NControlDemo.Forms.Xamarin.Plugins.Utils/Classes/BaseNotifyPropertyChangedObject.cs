/****************************** Module Header ******************************\
Module Name:  BaseNotifyPropertyChangedObject.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using NControlDemo.Helpers;

namespace NControlDemo.Classes
{
	/// <summary>
	/// Base class with propertynotify changed
	/// </summary>
	public class BaseNotifyPropertyChangedObject: INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		/// <summary>
		/// Occurs when property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Protected Members

		/// <summary>
		/// Calls the notify property changed event if it is attached. By using some
		/// Expression/Func magic we get compile time type checking on our property
		/// names by using this method instead of calling the event with a string in code.
		/// </summary>
		/// <param name="property">Property.</param>
		protected void RaisePropertyChangedEvent(Expression<Func<object>> property)
		{
			if (PropertyChanged == null)
				return;

			PropertyChanged(this, new PropertyChangedEventArgs(
				PropertyNameHelper.GetPropertyName<BaseNotifyPropertyChangedObject>(property)));
		}

		#endregion
	}
}

