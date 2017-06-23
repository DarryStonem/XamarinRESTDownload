using Microsoft.Practices.Unity;
using MobileClient.Services;
using MobileClient.Views;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MobileClient
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("NavigationPage/HomeView");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<HomeView>();
            Container.RegisterTypeForNavigation<PDFView>();

            Container.RegisterType<IPDFService, PDFService>(new ContainerControlledLifetimeManager());
        }
    }
}
