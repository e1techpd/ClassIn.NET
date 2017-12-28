using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.E1Technology.EEO.Model
{
    public class CourseDetail
    {
        public string ClassName { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TeacherAccount { get; set; }
        public string TeacherName { get; set; }
        public Int64? FolderId { get; set; }
        public int? SeatNum { get; set; }
        public bool Record { get; set; }
        public bool Live { get; set; }
        public bool Replay  { get; set; }
        public string AssistantAccount{ get; set; }
        public string CustomColumn { get; set; }
    }
}
