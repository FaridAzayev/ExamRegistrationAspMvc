using ExamRegistrationAspMvc.Models;
using ExamRegistrationAspMvc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamRegistrationAspMvc.Controllers
{
    public class ExamsController : Controller
    {
        private static DaoService daoService = new DaoService();
        private static List<Exam> _exams = new List<Exam>();

        private Exam FindOneByCode(int num, string code)
        {
            return daoService.GetExams().Where(e => e.StudentNo == num && e.SubjectCode.Trim().Equals(code)).FirstOrDefault();
        }

        [Route("Exams")]
        [Route("Exams/Index")]
        public ActionResult Index()
        {
            return View(daoService.GetExams());
        }


        [HttpGet]
        [Route("Exams/Add")]
        public ActionResult Add()
        {
            return View(new Exam());
        }

        [HttpPost]
        [Route("Exams/Add")]
        public ActionResult Add(Exam model)
        {
            daoService.InsertExam(model);
            return RedirectToAction("Index", "Exams");
        }

        [Route("Exams/Delete/{number}/{code}")]
        public ActionResult Delete(int number, string code)
        {
            daoService.DeleteExam(number, code);
            return RedirectToAction("Index", "Exams");
        }
    }
}