using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamRegistrationAspMvc.Models
{
    public class Student
    {
        public int Number { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ClassNo { get; set; }
    }
}