using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AciLabTestApp.Models;
using System.Net.Http;

namespace AciLabTestApp.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        public ActionResult Tutorials(int? id)
        {
            var userLogin = (LoginViewModel) Session["login"];

            if (userLogin != null)
            {
                var getTutorialList = new List<TutorialViewModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.GetAsync("Student/getTutorials?id=" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<TutorialViewModel>>();
                        readTask.Wait();

                        getTutorialList = (List<TutorialViewModel>) readTask.Result;

                        return View(getTutorialList);

                    }
                    //else //web api sent error response 
                    //{
                    //    //log response status here..


                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    return View(getTutorialList);
                    //}
                }
                
            }
            return RedirectToAction("Login", "Login");
        }
	}
}