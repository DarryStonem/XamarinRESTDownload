using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Services
{
    public interface IDownloadFileService
    {
        int Progress { get; set; }
        Task<string> DownloadAsync(string url, string name);
    }
}
