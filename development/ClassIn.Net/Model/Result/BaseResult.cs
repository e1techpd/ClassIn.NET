using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.E1Technology.EEO.Model
{
    public class BaseResult
    {
        [JsonProperty(PropertyName = "customColumn")]
        public string CustomColumn { get; set; }
        [JsonProperty(PropertyName = "errno")]
        public int Errno { get; set; }
        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }
    }
}
