using MobileClient.Models;
using MobileClient.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private INavigationService _navigationService;
        private IPDFService _pdfService;
        private readonly IPageDialogService _dialogService;

        ObservableCollection<DocumentModel> _documentsList;
        DocumentModel _selectedDocument;
        string _title;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public DocumentModel SelectedDocument
        {
            get => _selectedDocument;
            set => SetProperty(ref _selectedDocument, value);
        }

        public ObservableCollection<DocumentModel> DocumentsList
        {
            get => _documentsList;
            set => SetProperty(ref _documentsList, value);
        }

        public DelegateCommand<DocumentModel> DownloadCommand { get; set; }

        public HomeViewModel(INavigationService navigationService, IPDFService pdfService, IPageDialogService dialogService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _pdfService = pdfService;

            DownloadCommand = new DelegateCommand<DocumentModel>(DownloadItem);
        }
        
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            GetDocumentsFromService();
            Title = "Welcome";
        }

        private async void GetDocumentsFromService()
        {
            try
            {
                if (DocumentsList == null)
                    DocumentsList = new ObservableCollection<DocumentModel>();
                else
                    return;

                DocumentsList.Clear();

                var items = await _pdfService.GetDocuments();
                if (items == null)
                    return;

                foreach (var item in items)
                {
                    DocumentsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                await _dialogService.DisplayAlertAsync("My App", ex.Message, "Ok");
            }
        }

        private async void DownloadItem(DocumentModel obj)
        {
            var navigationParams = new NavigationParameters
            {
                { "name", obj.Name },
                { "url", obj.Url }
            };

            await _navigationService.NavigateAsync("PDFView", navigationParams);
        }
    }
}
