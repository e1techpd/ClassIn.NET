using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EF.E1Technology.EEO
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorInfo
    {
        /// <summary>
        /// Gets or sets the error number.
        /// </summary>
        /// <value>
        /// The error number.
        /// </value>
        [JsonProperty(PropertyName = "errno")]
        public int ErrorNumber { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        [JsonProperty(PropertyName = "error")]
        public string ErrorMessage { get; set; }
    }
}
