using System;
using System.Threading.Tasks;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Services
{
    /// <summary>
    /// I preference service.
    /// </summary>
    public interface IPreferenceService: IService
    {
        /// <summary>
        /// Persists the data to disk
        /// </summary>
        /// <returns>The async.</returns>
        Task PersistAsync();
    }
}

