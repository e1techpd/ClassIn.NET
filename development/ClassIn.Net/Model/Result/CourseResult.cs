using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.E1Technology.EEO.Model
{
    public class CourseResult: BaseResult
    {
        [JsonProperty(PropertyName = "data")]
        public Int64 Data { get; set; }
        [JsonProperty(PropertyName = "className")]
        public string ClassName { get; set; }
    }
}
