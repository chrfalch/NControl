/****************************** Module Header ******************************\
Module Name:  IViewWithParameter.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Threading.Tasks;

namespace NControlDemo.FormsApp.Mvvm
{
	public interface IViewWithParameter: IView
	{
		/// <summary>
		/// Initializes the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="parameter">Parameter.</param>
		/// <typeparam name="TParameter">The 1st type parameter.</typeparam>
		Task InitializeAsync (object parameter);
	}
}

