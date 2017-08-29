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

            if (userLogin != null && id != null && id != 0)
            {
                ViewBag.Id = id;
                var getTutorialList = new List<TutorialViewModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.GetAsync("Student/getTutorials?id=" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<TutorialViewModel>>();
                        readTask.Wait();

                        getTutorialList =  readTask.Result;

                        return View(getTutorialList);
                    }
                    
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                    return View(getTutorialList);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult CreateTutorial(int? id)
        {
            var userLogin = (LoginViewModel) Session["login"];

            if (userLogin != null && id != null && id != 0)
            {
                ViewBag.stdId = id;
                ViewBag.Message = "";
                var tuModel = new TutorialViewModel
                {
                    StudentId = Convert.ToInt32(id)
                };

                return View(tuModel);
            }
            return RedirectToAction("Login", "Login");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTutorial([Bind(Include = "TutorialId,StudentId,Complete")] TutorialViewModel model)
        {
            var userLogin = (LoginViewModel) Session["login"];

            if (userLogin != null && model.StudentId != 0)
            {
                HttpPostedFileBase file = Request.Files["fileUpload"];

                if (file.FileName != "" && file.FileName != null)
                {
                    using (var client = new HttpClient())
                    {
                        using (var content = new MultipartFormDataContent())
                        {
                            byte[] Bytes = new byte[file.InputStream.Length + 1];
                            file.InputStream.Read(Bytes, 0, Bytes.Length);
                            var fileContent = new ByteArrayContent(Bytes);
                            fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment") { FileName = file.FileName };
                            content.Add(fileContent);

                            client.BaseAddress = new Uri("http://localhost:1582/api/");

                            var responseTask = client.PostAsync("Student/FileUpload", content);
                            responseTask.Wait();

                            var result = responseTask.Result;

                            if (result.IsSuccessStatusCode)
                            {
                                var customerJsonString = result.Content.ReadAsStringAsync();
                                string getName = customerJsonString.Result;

                                model.FileName = getName.Trim('"');

                                using (var client2 = new HttpClient())
                                {
                                    client2.BaseAddress = new Uri("http://localhost:1582/api/");

                                    var responseTask2 = client.PostAsJsonAsync("Student/AddTutorial", model);
                                    responseTask2.Wait();

                                    var result2 = responseTask2.Result;
                                    if (result2.IsSuccessStatusCode)
                                    {
                                        ViewBag.Message = "File Uploaded Successfully";
                                        ViewBag.Error = false;
                                        model.FileName = string.Empty;
                                        model.Complete = false;
                                    }
                                }
                            }
                            else
                            {
                                ViewBag.Message = "File Uploaded Failed";
                                model.FileName = string.Empty;
                                model.Complete = false;
                            }
                            return View(model);
                        }
                    }
                }
                ViewBag.Error = true;
                ViewBag.Message = "";
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }
        
        public ActionResult EditTutorial(int id)
        {
            return View();
        }
        
        public ActionResult Courses(int? id)
        {
            var userLogin = (LoginViewModel)Session["login"];

            if (userLogin != null && id != null && id != 0)
            {
                ViewBag.Id = id;
                var completeCourseList = new List<CompleteCourseViewModel>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.GetAsync("Student/GetCompleteCourse?id=" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<CompleteCourseViewModel>>();
                        readTask.Wait();

                        completeCourseList = readTask.Result;
                        return View(completeCourseList);
                    }

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return View(completeCourseList);
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult CreateCourse(int? id)
        {
            var cmpCoursModel = new CompleteCourseViewModel();
            if (id != 0 && id != null)
            {
                cmpCoursModel.StudentId = (int) id;
                cmpCoursModel.Status = false;
            }

            var getcourses = GetMultipleCourse(id);

            ViewBag.CourseList = new MultiSelectList(getcourses, "CourseId", "CourseName");
            ViewBag.getId = id;
            ViewBag.Message = "";

            return View(cmpCoursModel);
        }

        public List<CourseViewModel> GetMultipleCourse(int? id)
        {
            var courses = new List<CourseViewModel>();

            if (id != null && id != 0)
            {
                using (var client = new HttpClient())
                {
                    var remainCourseList = new List<CourseViewModel>();

                    client.BaseAddress = new Uri("http://localhost:1582/api/");

                    var responseTask = client.GetAsync("Student/GetIncompleteCourses?id=" + id);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<List<CourseViewModel>>();
                        readTask.Wait();

                        remainCourseList = readTask.Result;
                    }

                    courses = remainCourseList.Select(c => new CourseViewModel()
                    {
                        CourseId = c.CourseId,
                        CourseName = c.CourseName
                    }).ToList();

                }
            }
            
            return courses;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(int[] CourseId, CompleteCourseViewModel model)
        {
            if (CourseId != null)
            {
                int len = CourseId.Length;
                for (int k = 0; k < len; k++)
                {
                    var newModel = new CompleteCourseViewModel
                    {
                        CourseId = CourseId[k],
                        StudentId = model.StudentId,
                        Status = model.Status
                    };
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:1582/api/");

                        var responseTask = client.PostAsJsonAsync("Student/AddIncompleteCourses", newModel);
                        responseTask.Wait();

                        var result = responseTask.Result;

                        if (result.IsSuccessStatusCode) { }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                            break;
                        }
                    }
                }

                ViewBag.Message = "Course are added Successfully";
                var updatedcourses = GetMultipleCourse(model.StudentId);
                ViewBag.CourseList = new MultiSelectList(updatedcourses, "CourseId", "CourseName");
                ViewBag.getId = model.StudentId;
                ViewBag.Error = false;
                return View(model);
            }

            var courses = GetMultipleCourse(model.StudentId);
            ViewBag.CourseList = new MultiSelectList(courses, "CourseId", "CourseName");
            ViewBag.getId = model.StudentId;
            ViewBag.Error = true;
            return View(model);
        }

        public ActionResult Editcourse(int? id)
        {
            return View();
        }

        public ActionResult Results(int? id)
        {
            {
                var userLogin = (LoginViewModel)Session["login"];

                if (userLogin != null && id != null && id != 0)
                {
                    ViewBag.Id = id;
                    var getResultList = new List<ResultViewModel>();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:1582/api/");

                        var responseTask = client.GetAsync("Student/getResults?id=" + id);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<List<ResultViewModel>>();
                            readTask.Wait();

                            getResultList = readTask.Result;

                            return View(getResultList);
                        }

                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");

                        return View(getResultList);
                    }
                }
                return RedirectToAction("Login", "Login");
            }
        }

        public ActionResult CreateResult(int? id)
        {
            var userLogin = (LoginViewModel)Session["login"];

            if (userLogin != null && id != null && id != 0)
            {
                ViewBag.stdId = id;
                ViewBag.Message = "";
                var resModel = new ResultViewModel()
                {
                    StudentId = Convert.ToInt32(id)
                };

                return View(resModel);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateResult([Bind(Include = "ResultId,StudentId,SemesterName,Grade")] ResultViewModel model)
        {
            var userLogin = (LoginViewModel)Session["login"];

            if (userLogin != null && model.StudentId != 0)
            {
                if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:1582/api/");

                        var responseTask = client.PostAsJsonAsync("Student/savedResult", model);
                        responseTask.Wait();

                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Add Result Successfully";
                            ViewBag.Error = false;
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        }
                    }
                }
                return View(model);
            }
            return RedirectToAction("Login", "Login");
        }

        public ActionResult EditResult(int? id)
        {
            return View();
        }

	}
}