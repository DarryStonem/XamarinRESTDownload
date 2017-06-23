using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using MobileClient.Services;
using MobileClient.iOS.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using QuickLook;

[assembly: Dependency(typeof(DownloadFileServiceTouch))]
namespace MobileClient.iOS.Services
{
    public class DownloadFileServiceTouch : IDownloadFileService
    {
        public int Progress { get; set; }

        public async Task<string> DownloadAsync(string url, string name)
        {
            var webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            var result = await webClient.DownloadStringTaskAsync(new Uri(url));
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string localFilename = $"{name}.pdf";
            string localPath = Path.Combine(documentsPath, localFilename);
            File.WriteAllText(localPath, result);

            return localPath;
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
            MessagingCenter.Send(Progress.ToString(), "Download");
        }
    }
}