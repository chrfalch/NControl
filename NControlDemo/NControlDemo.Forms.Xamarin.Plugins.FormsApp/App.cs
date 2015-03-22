using System;

using Xamarin.Forms;
using NControlDemo.FormsApp.IoC;
using System.Threading.Tasks;
using NControlDemo.FormsApp.Views;
using NControlDemo.FormsApp.Providers;
using NControlDemo.FormsApp.ViewModels;
using NControlDemo.FormsApp.Mvvm;

namespace NControlDemo.FormsApp
{
    /// <summary>
    /// App.
    /// </summary>
	public class App : Application
	{                
        #region Private Members

        /// <summary>
        /// The initialized.
        /// </summary>
        private bool _initialized = false;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.App"/> class.
        /// </summary>
        /// <param name="typeResolveProvider">Type resolve provider.</param>
        public App (IContainerProvider containerProvider, Action<IContainerProvider> setupContainerCallback)
		{
            // Save container
            Container = containerProvider;

            // Only fill container if it has not been filled yet
            if (!_initialized)
            {
                _initialized = true;

                // Set up container
                SetupContainer();

                // Let the caller setup its container
                if (setupContainerCallback != null)
                    setupContainerCallback(Container);

                // Register views
                RegisterViews();
            }

            // The root page of your application
            MainPage = Container.Resolve<MainView>();
		}

        #region App Properties

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>The container.</value>
        public IContainerProvider Container { get; private set;}

        #endregion

        #region App Lifecycle Callbacks

        /// <summary>
        /// Application developers override this method to perform actions when the application starts.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnStart ()
		{
			// Handle when your app starts
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application enters the sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnSleep ()
		{
			
		}

        /// <summary>
        /// Application developers override this method to perform actions when the application resumes from a sleeping state.
        /// </summary>
        /// <remarks>To be added.</remarks>
		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        #endregion

        #region Private Members

        /// <summary>
        /// Setups the container.
        /// </summary>
        private void SetupContainer()
        {
            
        }

        /// <summary>
        /// Registers the views.
        /// </summary>
        private void RegisterViews()
        {
            ViewManager.RegisterView<MainViewModel, MainView>();
        }
        #endregion
	}
}

