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
using NControlDemo;

namespace NControlDemo.FormsApp.IoC
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

