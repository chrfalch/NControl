/****************************** Module Header ******************************\
Module Name:  ContainerHelper.cs
Copyright (c) Christian Falch
All rights reserved.

THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp.IoC
{
    /// <summary>
    /// Container helper.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Resolves the given type into an instance
        /// </summary>
        /// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
        public static TTypeToResolve Resolve<TTypeToResolve>() where TTypeToResolve: class
        {
            return (Application.Current as App).Container.Resolve<TTypeToResolve>();
        }

        /// <summary>
        /// Resolves the given type into an instance
        /// </summary>
        /// <typeparam name="TTypeToResolve">The 1st type parameter.</typeparam>
        public static object Resolve(Type typeToResolve)
        {
            return (Application.Current as App).Container.Resolve(typeToResolve);
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void RegisterType<RegisterType, RegisterImplementation> () where RegisterType : class 
            where RegisterImplementation : class, RegisterType
        {
            (Application.Current as App).Container.Register<RegisterType, RegisterImplementation>();
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void Register<RegisterType, RegisterImplementation> (RegisterImplementation implementation) 
            where RegisterType : class 
            where RegisterImplementation : class, RegisterType
        {
            (Application.Current as App).Container.Register<RegisterType, RegisterImplementation>(implementation);
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void Register(Type registerType, Type registerImplementation)
        {
            (Application.Current as App).Container.Register(registerType, registerImplementation);
        }

        /// <summary>
        /// Register a type as a singleton in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void RegisterSingleton<RegisterType, RegisterImplementation> () where RegisterType : class 
            where RegisterImplementation : class, RegisterType
        {
            (Application.Current as App).Container.RegisterSingleton<RegisterType, RegisterImplementation>();
        }

        /// <summary>
        /// Register a type as a singleton in the container.
        /// </summary>
        /// <typeparam name="RegisterType">The 1st type parameter.</typeparam>
        /// <typeparam name="RegisterImplementation">The 2nd type parameter.</typeparam>
        public static void RegisterSingleton(Type registerType, Type registerImplementation)
        {
            (Application.Current as App).Container.RegisterSingleton(registerType, registerImplementation);
        }
    }
}

