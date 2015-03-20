/****************************** Module Header ******************************\
Module Name:  ITypeResolveProvider.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.IoC
{
	/// <summary>
	/// Defines the container interface
	/// </summary>
	public interface IContainerProvider
	{
		/// <summary>
		/// Resolves the given type into an instance
		/// </summary>
		/// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
		TTypeToResolve Resolve<TTypeToResolve>() where TTypeToResolve: class;

		/// <summary>
		/// Resolves the given type into an instance
		/// </summary>
		/// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
		object Resolve(Type typeToResolve);

		/// <summary>
		/// Register a type in the container.
		/// </summary>
		/// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
		/// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
		void Register<RegisterType, RegisterImplementation> () 
            where RegisterType : class where RegisterImplementation : class, RegisterType;

		/// <summary>
		/// Register a type in the container.
		/// </summary>
		/// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
		/// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
		void Register(Type registerType, Type registerImplementation);

        /// <summary>
        /// Register the specified registerImplementation.
        /// </summary>
        /// <param name="registerImplementation">Register implementation.</param>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        void Register<RegisterType, RegisterImplementation>(RegisterImplementation registerImplementation)
            where RegisterType : class 
            where RegisterImplementation : class, RegisterType;

		/// <summary>
		/// Register a type as a singleton in the container.
		/// </summary>
		/// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
		/// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
		void RegisterSingleton<RegisterType, RegisterImplementation> () 
            where RegisterType : class where RegisterImplementation : class, RegisterType;

		/// <summary>
		/// Register a type as a singleton in the container.
		/// </summary>
		/// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
		/// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
		void RegisterSingleton(Type RegisterType, Type RegisterImplementation);
	}
}

