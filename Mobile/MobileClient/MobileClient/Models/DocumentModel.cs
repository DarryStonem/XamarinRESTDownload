using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Models
{
    public class DocumentModel
    {
        [JsonProperty("documentName")]
        public string Name { get; set; }

        [JsonProperty("documentUrl")]
        public string Url { get; set; }
    }
}
