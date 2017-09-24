using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    var readTask = result.Content.ReadAsAsync<List<CourseViewModel>>();
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


       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]       
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.PostAsJsonAsync("Course/savedCourse", model);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    //else //web api sent error response 
                    //{
                    //    //log response status here..

                       
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    //}
                }
                
            }

            return View(model);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CourseViewModel model;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:1582/api/");

                var responseTask = client.GetAsync("Course/getCourse?id="+id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CourseViewModel>();
                    readTask.Wait();

                    model = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model = null;

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
                        
            if (model == null)
            {
                return HttpNotFound();
            }
                       
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.PostAsJsonAsync("Course/updateCourse", model);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(model);
        }
	}
}