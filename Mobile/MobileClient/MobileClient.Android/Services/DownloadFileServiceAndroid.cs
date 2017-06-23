using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileClient.Services;
using Xamarin.Forms;
using MobileClient.Droid.Services;
using System.Net;
using System.IO;
using System.Threading.Tasks;

[assembly: Dependency(typeof(DownloadFileServiceAndroid))]
namespace MobileClient.Droid.Services
{
    public class DownloadFileServiceAndroid : IDownloadFileService
    {
        public int Progress { get; set; }

        public async Task<string> DownloadAsync(string url, string name)
        {
            var webClient = new WebClient();
            webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;
            var result = await webClient.DownloadStringTaskAsync(new Uri(url));
            var directory = global::Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            directory = Path.Combine(directory, Android.OS.Environment.DirectoryDownloads);
            string localFilename = $"{name}.pdf";
            string filePath = Path.Combine(directory.ToString(), localFilename);
            File.WriteAllText(filePath, result);

            return filePath;
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
            MessagingCenter.Send(Progress.ToString(), "Download");
        }
    }
}