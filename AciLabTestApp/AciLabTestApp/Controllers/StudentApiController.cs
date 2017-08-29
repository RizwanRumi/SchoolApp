using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            StudentViewModel studentVm = new StudentViewModel();

            studentVm = istudentDetails.AddNewStudent(model);

            return Json(studentVm);
        }

        [Route("getTutorials")]
        [HttpGet]
        public IHttpActionResult GetTutorialList(int id)
        {
            IList<TutorialViewModel> tutorials = new List<TutorialViewModel>();
            tutorials = istudentDetails.GetAllTutorial(id);

            return Json(tutorials);
        }
        
    }
}
