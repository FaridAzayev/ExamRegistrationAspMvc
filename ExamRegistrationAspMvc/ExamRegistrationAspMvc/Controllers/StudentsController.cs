using ExamRegistrationAspMvc.Models;
using ExamRegistrationAspMvc.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExamRegistrationAspMvc.Controllers
{
    public class StudentsController : Controller
    {
        private static DaoService daoService = new DaoService();
        private static List<Student> _students = new List<Student>();

        private Student FindOneByCode(int number)
        {
            return daoService.GetStudents().Where(s => s.Number == number).FirstOrDefault();
        }

        [Route("Students")]
        [Route("Students/Index")]
        public ActionResult Index()
        {
            return View(daoService.GetStudents());
        }

        [HttpGet]
        [Route("Students/Edit/{number}")]
        public ActionResult Edit(int number)
        {
            return View(FindOneByCode(number));
        }

        [HttpPost]
        [Route("Students/Edit/{student}")]
        public ActionResult Edit(Student model)
        {
            daoService.UpdateStudent(model);
            return RedirectToAction("Index", "Students");
        }

        [HttpGet]
        [Route("Students/Add")]
        public ActionResult Add()
        {
            return View(new Student());
        }

        [HttpPost]
        [Route("Students/Add")]
        public ActionResult Add(Student model)
        {
            daoService.InsertStudent(model);
            return RedirectToAction("Index", "Students");
        }

        [Route("Students/Delete/{number}")]
        public ActionResult Delete(int number)
        {
            daoService.DeleteStudent(number);
            return RedirectToAction("Index", "Students");
        }
    }
}