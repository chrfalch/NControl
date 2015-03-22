using System;
using Xamarin.Forms;

namespace NControlDemo.FormsApp.Mvvm
{
    /// <summary>
    /// image provider.
    /// </summary>
    public interface IImageProvider
    {
        /// <summary>
        /// Returns an image asset loaded on the platform
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="imageName">Image name.</param>
        FileImageSource GetImageSource(string imageName);
    }
}

