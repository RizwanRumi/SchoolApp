using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AciLabTestApp.Models;
using System.Net.Http;

namespace AciLabTestApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Login()
        {
            Session["login"] = null;
            return View();
        }

        //public ActionResult Home()
        //{
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "StudentEmail,Password")] LoginViewModel model)
        {
            try
            {
                var userLogin = Session["login"];

                if (userLogin != null)
                {
                    return View("Home", model);
                }
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:1582/api/");

                        var responseTask = client.GetAsync("Student/getAllStudent");
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<List<StudentViewModel>>();
                            readTask.Wait();

                            var getStudentList = readTask.Result;

                            foreach (var stVm in getStudentList)
                            {
                                if (stVm.Email == model.StudentEmail && stVm.Password == model.Password)
                                {
                                    model.StudentId = stVm.StudentId;

                                    ViewBag.Name = stVm.Name;
                                    Session["Name"] = stVm.Name;
                                    Session["login"] = model;

                                    return View("Home", model); 
                                } 
                            }

                            ViewBag.Error = true;

                            return View("Login");

                        }
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }
                    Session["login"] = model;
                }
                else
                {
                    //ModelState.AddModelError(" ","Email Or password Is Incorrect");
                    @ViewBag.Error = true;
                    return View("Login");
                }
                return View("Home", model);
            }
            catch (Exception exp)
            {
                return View("Home", model);
            }

        }

        public ActionResult Register()
        {
            var userLogin = Session["login"];

            if (userLogin != null)
            {
                ViewBag.Name = Session["Name"];
                return View("Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name,Email,Address,Department,Batch,Enrolled,Password")] StudentViewModel model)
        {
            try
            {
                var userLogin = Session["login"];

                if (userLogin != null)
                {
                    return View("Home");
                }
                if (ModelState.IsValid)
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:1582/api/");

                        var responseTask = client.PostAsJsonAsync("Student/addStudent", model);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                             var readTask = result.Content.ReadAsAsync<StudentViewModel>();
                                readTask.Wait();

                           var getStudent = readTask.Result;

                           var loginModel = new LoginViewModel
                           {
                               StudentId = getStudent.StudentId,
                               StudentEmail = getStudent.Email,
                               Password = getStudent.Password
                           };

                            Session["login"] = loginModel;

                            Session["Name"] = getStudent.Name;

                            ViewBag.Name = Session["Name"];

                           return View("Home", loginModel);
                        }
                        //else //web api sent error response 
                        //{
                        //    //log response status here..


                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        //}
                    }
                    
                }
                else
                {
                    //ModelState.AddModelError(" ","Email Or password Is Incorrect");
                    @ViewBag.Error = true;
                    return View("Register");
                }
                return View("Home", model);
            }
            catch (Exception exp)
            {
                return View("Home", model);
            }


        }


        public ActionResult Logout()
        {
            Session["login"] = null;
            Session["Name"] = null;
            return RedirectToAction("Login", "Login");
        }
       
    }
}