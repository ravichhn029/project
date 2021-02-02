using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;
namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            List<Student> student = DBManager.GetAllStudents();
            this.ViewData["stud"] = student;

            return View();
        }

        public ActionResult Demo()
        {
            

            return View();
        }
    }
}