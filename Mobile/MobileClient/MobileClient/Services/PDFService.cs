using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileClient.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace MobileClient.Services
{
    public class PDFService : IPDFService
    {
        public async Task<List<DocumentModel>> GetDocuments()
        {
            var items = await GetDocumentsAsync();
            return items;
        }

        private async Task<List<DocumentModel>> GetDocumentsAsync()
        {
            var items = new List<DocumentModel>();

            using (var httpClient = new HttpClient())
            {
                var jsonResponse = await httpClient.GetStringAsync(StringResources.UrlService);
                if(String.IsNullOrEmpty(jsonResponse))
                    throw new ServiceException(StringResources.ErrorFromServiceException);
                var apiresponse = JsonConvert.DeserializeObject<List<DocumentModel>>(jsonResponse);
                return apiresponse;
            }
        }
    }
}
