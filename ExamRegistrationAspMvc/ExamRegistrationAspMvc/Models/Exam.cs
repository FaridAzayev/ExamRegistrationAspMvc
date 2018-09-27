using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamRegistrationAspMvc.Models
{
    public class Exam
    {
        public string SubjectCode { get; set; }
        public int StudentNo { get; set; }
        public DateTime ExamDate { get; set; }
        public int Mark { get; set; }
    }
}