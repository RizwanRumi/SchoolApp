using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AciLabTestApp.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        public ActionResult Index()
        {
            IEnumerable<CourseViewModel> courses = null;

            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1582/api/");

                var responseTask = client.GetAsync("Course/getCourses");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<CourseViewModel>>();
                    readTask.Wait();

                    courses = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    courses = Enumerable.Empty<CourseViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            return View(courses);
        }
	}
}