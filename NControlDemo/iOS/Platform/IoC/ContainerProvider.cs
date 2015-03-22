using System;
using TinyIoC;
using System.Collections.Generic;
using System.Diagnostics;
using NControlDemo.FormsApp.IoC;

namespace NControlDemo.iOS.Platform.IoC
{
    public class ContainerProvider: IContainerProvider
	{
		public ContainerProvider ()
		{
		}

        #region IContainerProvider implementation

		public TTypeToResolve Resolve<TTypeToResolve> () where TTypeToResolve : class
		{
			try 
			{			
				return TinyIoCContainer.Current.Resolve<TTypeToResolve> ();

			} catch (Exception ex) 
			{
				var msg = GetMessageFromInnerException (ex);
				Debugger.Log (0, "Resolver", msg);
				throw new Exception (msg, ex);
			}
		}

		public object Resolve (Type typeToResolve)
		{
			try 
			{			
				return TinyIoCContainer.Current.Resolve (typeToResolve);

			} catch (Exception ex) 
			{
				var msg = GetMessageFromInnerException (ex);
				Debugger.Log (0, "Resolver", msg);
				throw new Exception (msg, ex);
			}

		}

		public void Register<RegisterType, RegisterImplementation> () 
            where RegisterType : class 
            where RegisterImplementation : class, RegisterType
		{
            TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation> ().AsMultiInstance();
		}

		public void Register (Type registerType, Type registerImplementation)
		{
            TinyIoCContainer.Current.Register (registerType, registerImplementation).AsMultiInstance();
		}

		public void RegisterSingleton<RegisterType, RegisterImplementation> () 
            where RegisterType : class 
            where RegisterImplementation : class, RegisterType
		{
			TinyIoCContainer.Current.Register<RegisterType, RegisterImplementation> ().AsSingleton ();
		}

		public void RegisterSingleton (Type RegisterType, Type RegisterImplementation)
		{
			TinyIoCContainer.Current.Register (RegisterType, RegisterImplementation).AsSingleton ();
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
			var msg = new List<string> ();
			while (true) {
				msg.Insert (0, cur.Message);

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

