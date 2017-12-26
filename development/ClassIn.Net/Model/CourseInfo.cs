using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EF.E1Technology.EEO
{
    public class CourseInfo
    {
        /// <summary>
        /// Gets or sets the name of the course.
        /// </summary>
        /// <value>
        /// The name of the course.
        /// </value>
        [JsonProperty(PropertyName = "course_name")]
        public string CourseName { get; set; }

        /// <summary>
        /// Gets or sets the created stamp.
        /// </summary>
        /// <value>
        /// The created stamp.
        /// </value>
        [JsonProperty(PropertyName = "create_time")]
        [JsonConverter(typeof(UnixTimeSerializer))]
        public DateTime? CreatedStamp { get; set; }

        /// <summary>
        /// Gets or sets the expired stamp.
        /// </summary>
        /// <value>
        /// The expired stamp.
        /// </value>
        [JsonProperty(PropertyName = "expiry_time")]
        [JsonConverter(typeof(UnixTimeSerializer))]
        public DateTime? ExpiredStamp { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonProperty(PropertyName = "status")]
        public CourseStatus Status { get; set; }
    }
}
