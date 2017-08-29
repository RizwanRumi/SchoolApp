using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using AciLabTestApp.Models;
using System.Net.Http;
using Newtonsoft.Json;

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

        public ActionResult CreateTutorial(int? id)
        {
            var userLogin = (LoginViewModel) Session["login"];

            if (userLogin != null && id != null && id != 0)
            {
                ViewBag.stdId = id;
                ViewBag.Message = "";
                var tuModel = new TutorialViewModel();
                tuModel.StudentId = Convert.ToInt32(id);

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

                if (file != null)
                {
                    using (HttpClient client = new HttpClient())
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
	}
}