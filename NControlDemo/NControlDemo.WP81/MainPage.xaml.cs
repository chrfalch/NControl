using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NControlDemo.WP81.Resources;
using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms;
using NControlDemo.FormsApp.Mvvm;
using NControlDemo.WP81.Platform.Mvvm;
using NControlDemo.WP81.Platform.IoC;

namespace NControlDemo.WP81
{
    public partial class MainPage : FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;
            Forms.Init();
            Xamarin.FormsMaps.Init();

            NControl.WP81.NControlViewRenderer.Init();

            LoadApplication(new NControlDemo.FormsApp.App(new ContainerProvider(), (container) =>
            {
                // Register providers
                container.Register<IImageProvider, ImageProvider>();

            }));
        }        
    }
}