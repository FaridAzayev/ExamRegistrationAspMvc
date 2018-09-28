using ExamRegistrationAspMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExamRegistrationAspMvc.Extensions;
using ExamRegistrationAspMvc.Repository;

namespace ExamRegistrationAspMvc.Controllers
{
    public class SubjectsController : Controller
    {
        private static DaoService daoService = new DaoService();
        private static List<Subject> _subjects = new List<Subject>();

        private Subject FindOneByCode(string code)
        {
            return daoService.GetSubjects().Where(s => s.Code.Trim().Equals(code)).FirstOrDefault();
        }

        [Route("Subjects")]
        [Route("Subjects/Index")]
        public ActionResult Index()
        {
            return View(daoService.GetSubjects());
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
            daoService.UpdateSubject(model);
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
            daoService.InsertSubject(model);
            return RedirectToAction("Index", "Subjects");
        }

        [Route("Subjects/Delete/{code}")]
        public ActionResult Delete(string code)
        {
            daoService.DeleteSubject(code);
            return RedirectToAction("Index","Subjects");
        }
    }
}