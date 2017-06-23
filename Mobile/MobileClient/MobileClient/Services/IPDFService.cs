using MobileClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Services
{
    public interface IPDFService
    {
        Task<List<DocumentModel>> GetDocuments();
    }
}
