using System;
using System.Collections.Generic;

using Xamarin.Forms;
using NControlDemo.FormsApp.ViewModels;
using NControlDemo.FormsApp.IoC;

namespace NControlDemo.FormsApp.Views
{
    public partial class MainViewXaml : ContentPage
    {
        public MainViewXaml()
        {
            InitializeComponent();

            ViewModel = Container.Resolve<MainViewModel> ();
            BindingContext = ViewModel;
        }

        /// <summary>
        /// Returns the ViewModel
        /// </summary>
        public MainViewModel ViewModel{get; private set;}
    }
}

