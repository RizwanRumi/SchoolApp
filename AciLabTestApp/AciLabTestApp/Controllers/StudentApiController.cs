using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using AciLabTestApp.IBLL;
using AciLabTestApp.Models;

namespace AciLabTestApp.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentApiController : ApiController
    {
        private IStudent istudentDetails;

        public StudentApiController(IStudent _istudentDetails)
        {
            istudentDetails = _istudentDetails;
        }

        [Route("getAllStudent")]
        [HttpGet]
        public IHttpActionResult GetStudentList()
        {
            IList<StudentViewModel> studentList = null;

            studentList = istudentDetails.GetAllStudent();

            return Json(studentList);
        }

        [Route("addStudent")]
        [HttpPost]
        public IHttpActionResult PostStudent(StudentViewModel model)
        {
            StudentViewModel studentVm = istudentDetails.AddNewStudent(model);

            return Json(studentVm);
        }

        [Route("getTutorials")]
        [HttpGet]
        public IHttpActionResult GetTutorialList(int id)
        {
            IList<TutorialViewModel> tutorials = istudentDetails.GetAllTutorial(id);

            return Json(tutorials);
        }


        [Route("FileUpload")]
        [HttpPost]
        public IHttpActionResult PostFileUpload()
        {
            string getFileName = "";

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {

                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];

                    if (postedFile != null)
                    {
                        var imgname = Path.GetFileName(postedFile.FileName);
                        var extension = Path.GetExtension(imgname);

                        Random _r = new Random();
                        var randomId = _r.Next();

                        string[] fileName = imgname.Split('.');

                        getFileName = fileName[0] + "_" + randomId + extension;


                        var filePath = HttpContext.Current.Server.MapPath("~/UploadFiles/" + getFileName);
                        postedFile.SaveAs(filePath);
                    }
                }

                return Json(getFileName);

            }

            return Json(HttpStatusCode.BadRequest);
        }


        [Route("AddTutorial")]
        [HttpPost]
        public IHttpActionResult PostTutorial(TutorialViewModel tmodel)
        {
           bool res = istudentDetails.AddTutotrial(tmodel);
            
           return Json(res);
        }

        [Route("GetCompleteCourse")]
        [HttpGet]
        public IHttpActionResult GetCompleteCourseList(int id)
        {
            IList<CompleteCourseViewModel> cmpCourseList = istudentDetails.GetAllCompletedCourse(id);

            return Json(cmpCourseList);
        }

        [Route("GetIncompleteCourses")]
        [HttpGet]
        public IHttpActionResult GetRemainCourseList(int id)
        {
            IList<CourseViewModel> cmpCourseList = istudentDetails.GetRemainCourseList(id);

            return Json(cmpCourseList);
        }

        [Route("AddIncompleteCourses")]
        [HttpPost]
        public IHttpActionResult PostAddRemainCourseList(CompleteCourseViewModel model)
        {
            bool result = istudentDetails.AddCompleteCourseViewModel(model);

            return Json(result);
        }
    }
}
