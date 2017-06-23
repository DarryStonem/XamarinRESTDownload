using MobileClient.Models;
using MobileClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void btnDownload_Clicked(object sender, EventArgs e)
        {
            var vm = (this.BindingContext as HomeViewModel);

            vm.DownloadCommand.Execute((sender as Button).BindingContext as DocumentModel);
        }
    }
}