using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using NControlDemo.FormsApp.Mvvm;
using NControlDemo.WP80.Platform.IoC;
using NControlDemo.WP80.Platform.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

namespace NControlDemo.WP80
{
    public partial class MainPage 
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            
            Forms.Init();
            Xamarin.FormsMaps.Init();

            NControl.WP80.NControlViewRenderer.Init();

            LoadApplication(new NControlDemo.FormsApp.App(new ContainerProvider(), (container) =>
            {
                // Register providers
                container.Register<IImageProvider, ImageProvider>();

            }));   
        }
    }
}