using System;

using Xamarin.Forms;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.IoC;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Services;
using NControlDemo.Forms.Xamarin.Plugins.Data.Services;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;
using NControlDemo.Forms.Xamarin.Plugins.Data.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Repositories;
using System.Threading.Tasks;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Views;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Providers;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.ViewModels;
using NControlDemo.Forms.Xamarin.Plugins.FormsApp.Mvvm;

namespace NControlDemo.Forms.Xamarin.Plugins.FormsApp
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
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.App"/> class.
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

            // Initialize 
            Task.Run(async() => {

                // Initialize services
                await Container.Resolve<IPreferenceService>().InitializeAsync().ContinueWith(t => 
                    Container.Resolve<ILoggingService>().InitializeAsync());

            });

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
			// Persist preferences
            Task.Run(async () => await Container.Resolve<IPreferenceService>().PersistAsync()).Wait();
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
            // Services
            Container.RegisterSingleton<IPreferenceService, PreferenceService>();
            Container.RegisterSingleton<ILoggingService, LoggingService>();

            // Repositories
            Container.RegisterSingleton<IRepository<PreferenceModel>, Repository<PreferenceModel>>();
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

