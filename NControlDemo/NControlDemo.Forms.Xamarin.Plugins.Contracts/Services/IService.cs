using System;
using System.Threading.Tasks;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Services
{
    /// <summary>
    /// Base service interface
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Initializes the async.
        /// </summary>
        /// <returns>The async.</returns>
        Task InitializeAsync();
    }
}

