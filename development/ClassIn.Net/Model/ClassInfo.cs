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
    public class ClassInfo
    {
        /// <summary>
        /// Gets or sets the name of the class.
        /// </summary>
        /// <value>
        /// The name of the class.
        /// </value>
        [JsonProperty(PropertyName = "class_name")]
        public string ClassName { get; set; }

        /// <summary>
        /// Gets or sets the name of the teacher.
        /// </summary>
        /// <value>
        /// The name of the teacher.
        /// </value>
        [JsonProperty(PropertyName = "teacher_name")]
        public string TeacherName { get; set; }

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
