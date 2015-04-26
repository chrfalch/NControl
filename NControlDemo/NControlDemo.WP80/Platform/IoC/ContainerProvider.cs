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
using NControlDemo.FormsApp.IoC;
using TinyIoC;
using System.Collections.Generic;
using System.Diagnostics;

namespace NControlDemo.WP80.Platform.IoC
{
    public class ContainerProvider : IContainerProvider
    {
        public ContainerProvider()
        {
        }

        #region IContainerProvider implementation

        public TTypeToResolve Resolve<TTypeToResolve>() where TTypeToResolve : class
        {
            try
            {
                return TinyIoCContainer.Current.Resolve<TTypeToResolve>();

            }
            catch (Exception ex)
            {
                var msg = GetMessageFromInnerException(ex);
                Debugger.Log(0, "Resolver", msg);
                throw new Exception(msg, ex);
            }
        }

        public object Resolve(Type typeToResolve)
        {
            try
            {
                return TinyIoCContainer.Current.Resolve(typeToResolve);

            }
            catch (Exception ex)
            {
                var msg = GetMessageFromInnerException(ex);
                Debugger.Log(0, "Resolver", msg);
                throw new Exception(msg, ex);
            }

        }

        public void Register<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType
        {
            TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation>().AsMultiInstance();
        }

        public void Register(Type registerType, Type registerImplementation)
        {
            TinyIoCContainer.Current.Register(registerType, registerImplementation).AsMultiInstance();
        }

        public void RegisterSingleton<RegisterType, RegisterImplementation>()
            where RegisterType : class
            where RegisterImplementation : class, RegisterType
        {
            TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation>().AsSingleton();
        }

        public void RegisterSingleton(Type RegisterType, Type RegisterImplementation)
        {
            TinyIoCContainer.Current.Register(RegisterType, RegisterImplementation).AsSingleton();
        }

        public void Register<RegisterType, RegisterImplementation>(RegisterImplementation registerImplementation)
            where RegisterType : class
            where RegisterImplementation : class, RegisterType
        {
            TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation>(registerImplementation);
        }

        #endregion

        #region Private Members

        private string GetMessageFromInnerException(Exception ex)
        {
            var cur = ex;
            var msg = new List<string>();
            while (true)
            {
                msg.Insert(0, cur.Message);

                if (cur.InnerException == null)
                    break;

                cur = cur.InnerException;
            }

            string result = string.Empty;
            foreach (var m in msg)
                result += m + System.Environment.NewLine;

            return result;
        }
        #endregion
    }
}

