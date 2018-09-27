using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamRegistrationAspMvc.Models
{
    public class Subject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int ClassNo { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }
    }
}