using Newtonsoft.Json;

namespace EF.E1Technology.EEO
{
    public class EEOResponse<T>
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the error information.
        /// </summary>
        /// <value>
        /// The error information.
        /// </value>
        [JsonProperty(PropertyName = "error_info")]
        public ErrorInfo ErrorInfo { get; set; }
    }
}
