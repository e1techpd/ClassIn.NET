using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.E1Technology.EEO
{
    public class StudentInfo
    {
        [JsonProperty(PropertyName = "student_account")]
        public Int64 StudentAccount { get; set; }

        [JsonProperty(PropertyName = "student_name")]
        public string StudentName { get; set; }

        [JsonProperty(PropertyName = "attendance")]
        public string Attendance { get; set; }

        [JsonProperty(PropertyName = "total_class")]
        public string TotalClass { get; set; }

        [JsonProperty(PropertyName = "on_class")]
        public int OnClass { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "studentId")]
        public Int64 StudentId { get;set; }

        [JsonProperty(PropertyName = "studentUid")]
        public Int64 StudentUid { get; set; }


    }
}
