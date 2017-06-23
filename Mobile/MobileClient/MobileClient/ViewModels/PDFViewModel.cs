using MobileClient.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileClient.ViewModels
{
    public class PDFViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly IDownloadFileService _downloadFileService;
        string _title;
        double _progress;

        public double Progress
        {
            get => _progress;
            set => SetProperty(ref _progress, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public PDFViewModel(IPageDialogService dialogService, IDownloadFileService downloadFileService)
        {
            _dialogService = dialogService;
            _downloadFileService = downloadFileService;
        }

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var name = (string)parameters["name"];
            var url = (string)parameters["url"];

            Title = name;

            GetPDF(url);
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            MessagingCenter.Unsubscribe<string>(this, "Download");
        }

        private async void GetPDF(string url)
        {
            MessagingCenter.Subscribe<string>(this, "Download", DownloadProgress);
            var file = await _downloadFileService.DownloadAsync(url, Title);

            if (!String.IsNullOrEmpty(file))
                await _dialogService.DisplayAlertAsync("My App", "The PDF has been downloaded", "OK");
            else
                await _dialogService.DisplayAlertAsync("My App", "The PDF was not downloaded", "OK");
        }

        private void DownloadProgress(string obj)
        {
            var progress = Double.Parse(obj) / 100;
            Progress = progress;
        }
    }
}
