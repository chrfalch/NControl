using System;
using NControlDemo.FormsApp.Mvvm;
using Xamarin.Forms;

namespace NControlDemo.iOS.Platform.Mvvm
{
    /// <summary>
    /// Image provider.
    /// </summary>
    public class ImageProvider: IImageProvider
    {
        #region IImageProvider implementation

        /// <summary>
        /// Returns an image asset loaded on the platform
        /// </summary>
        /// <returns>The image.</returns>
        /// <param name="imageName">Image name.</param>
        public FileImageSource GetImageSource(string imageName)
        {
            return imageName;
        }

        #endregion
    }
}

