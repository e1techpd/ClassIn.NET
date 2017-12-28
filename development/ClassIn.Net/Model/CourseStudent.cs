using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.E1Technology.EEO.Model
{
    public class CourseStudent
    {
        public Int64 CourseId { get; set; }
        public IdentityType Identity { get; set; }
        public bool IsRegister { get; set; }
        public List<Student> StudentList { get; set;}
    }
}
