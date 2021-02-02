using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetResultByROllNo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetResultByROllNo(int rollno)
        {
            Result result = DBManager.GetByRollno(rollno);
            Student s2 = DBManager.GetByID(rollno);
            StudentResult r1 = new StudentResult();
            r1.RollNo = result.RollNo;
            r1.Name = s2.Name;
            r1.Branch = s2.Branch;
            r1.Dob = s2.Dob;
            r1.FatherName = s2.FatherName;
            r1.Gender = s2.Gender;
            r1.English = result.English;
            r1.Java = result.Java;
            r1.Angular = result.Angular;
            r1.Html = result.Html;
            r1.DotNet = result.DotNet;
            
            
            TempData["result"] = r1;           
            return RedirectToAction("Details", "Student");
            
         
          
        }

        public ActionResult GetAllStudents()
        {
            
            return View();
        }

        public ActionResult Details()
        {
            StudentResult r = TempData["result"] as StudentResult;
            return View(r);
        }


    }
}