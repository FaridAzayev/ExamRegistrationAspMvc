using ExamRegistrationAspMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamRegistrationAspMvc.Extensions;

namespace ExamRegistrationAspMvc.Controllers
{
    public class SubjectsController : Controller
    {
        //private static List<Subject> _subjects = new List<Subject>();

        private static List<Subject> _subjects = new List<Subject>(new Subject[] {
            new Subject() { ClassNo = 1, Code = "test", Name = "testName" , TeacherFirstName= "Tname", TeacherLastName = "Tsurname"}
            , new Subject() { ClassNo = 2, Code = "test2", Name = "testName2" , TeacherFirstName= "Tname2", TeacherLastName = "Tsurname2"}
        });

        private Subject FindOneByCode(string code)
        {
            return _subjects.Where(s => s.Code.Equals(code)).FirstOrDefault();
        }

        [Route("Subjects")]
        [Route("Subjects/Index")]
        public ActionResult Index()
        {
            return View(_subjects);
        }

        [Route("Subjects/Get")]
        public ActionResult Get()
        {
            return Json(new { data = _subjects}, JsonRequestBehavior.AllowGet);
        }


        [Route("Subjects/Get/{code}")]
        public ActionResult Get(string code)
        {
            return Json(new { data = FindOneByCode(code)}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Subjects/Edit/{code}")]
        public ActionResult Edit(string code)
        {            
            return View(FindOneByCode(code));
        }

        [HttpPost]
        [Route("Subjects/Edit/{subject}")]
        public ActionResult Edit(Subject model)
        {
            //todo: update database
            FindOneByCode(model.Code).Copy(model);
            return RedirectToAction("Index", "Subjects");
        }


        [HttpGet]
        [Route("Subjects/Add")]
        public ActionResult Add()
        {
            return View(new Subject());
        }

        [HttpPost]
        [Route("Subjects/Add")]
        public ActionResult Add(Subject model)
        {
            _subjects.Add(model);
            return RedirectToAction("Index", "Subjects");
        }

        [Route("Subjects/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            //todo: delete in database
            _subjects.Remove(FindOneByCode(code));
            return RedirectToAction("Index","Subjects");
        }
    }
}