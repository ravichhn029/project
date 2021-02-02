using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BOL;
using DAL;

namespace WebApplication1.Controllers
{
    public class DocumentController : Controller
    {
        // GET: Document
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InsertResult()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertResult(int rollno,int english,int java,int angular,
            int dotnet,int html)
        {
            Result newresult = new Result()
            {
                RollNo = rollno,
                English = english,
                Java = java,
                Angular = angular,
                DotNet = dotnet,
                Html = html
            };
            TryValidateModel(newresult);
            if (ModelState.IsValid)

            {
                bool status = DBManager.InsertResult(newresult);
                if (status)
                {
                    return this.RedirectToAction("InsertResult", "Document");
                }
            }
            return View();
        }
           

        public ActionResult RegisterStudents()
        {
            List<Student> theStudent = DBManager.GetAllStudents();
            ViewData["student"]=theStudent;
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(int rollno,string branch,string name,
            string gender,string father, DateTime dob)
        {
            Student newstudent = new Student()
            {
                RollNo = rollno,
                Branch = branch,
                Name = name,
                Gender = gender,
                FatherName = father,
                Dob = dob
            };
            TryValidateModel(newstudent);
            if (ModelState.IsValid)

            {
                bool status = DBManager.Insert(newstudent);
                if (status)
                {
                    return this.RedirectToAction("InsertResult", "Document");
                }
            }
            return View();
        }



        public ActionResult Update(int rollno)
        {
            Student existingStudent = DBManager.GetByID(rollno);
            return View(existingStudent);
        }

        [HttpPost]
        public ActionResult Update(int rollno, string branch, string name,
            string gender, string father, DateTime dob)
        {
            Student existingstudent = new Student()
            {
                RollNo = rollno,
                Branch = branch,
                Name = name,
                Gender = gender,
                FatherName = father,
                Dob = dob
            };

            TryValidateModel(existingstudent);
            if (ModelState.IsValid)

            {
                bool status = DBManager.Update(existingstudent);
                if (status)
                {
                    return this.RedirectToAction("RegisterStudents", "Document");
                }
            }


            return View();
        }
        public ActionResult Delete(int rollno)
        {
            bool status = DBManager.Delete(rollno);
            if (status)
            {
                return RedirectToAction("RegisterStudents", "Document");
            }
            return View();
        }

    }
}