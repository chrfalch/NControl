using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NControl.Win
{
    public interface IPopupInformationProvider
    {
        /// <summary>
        /// Returns the parent for the current popup
        /// </summary>
        /// <returns></returns>
        FrameworkElement GetPopupParent();
    }
}
