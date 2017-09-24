using System.Data.Entity.Infrastructure;
using AciLabTestApp.IBLL;
using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AciLabTestApp.Controllers
{
    [RoutePrefix("api/Course")]
    public class CourseApiController : ApiController
    {
        ICourse icourseDetails;
        public CourseApiController(ICourse _icourseDetails)
        {
            icourseDetails = _icourseDetails;
        }

        [Route("getCourses")]
        [HttpGet]
        public IHttpActionResult GetCourseList()
        {
            IList<CourseViewModel> courseList = null;

            courseList = icourseDetails.GetAllCourses();

            return Json(courseList);
        }
        
        [Route("saveCourse")]
        [HttpPost]
        public IHttpActionResult PostCourse(CourseViewModel course)
        {
            string msg = "";
          
            bool res = icourseDetails.AddCourse(course);

            msg = res ? "Add Successfully" : "Error";

            return Json(msg);
        }
        
        [Route("getCourse")]
        [HttpGet]
        public IHttpActionResult GetCourse(int id)
        {
            CourseViewModel course = null;
            course = icourseDetails.GetCourse(id);
            
            return Json(course);
        }

        [Route("updateCourse")]
        [HttpPost]
        public IHttpActionResult PutCourse(CourseViewModel editCourse)
        {
            string msg = "";

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool res = icourseDetails.EditCourse(editCourse);

            msg = res ? "Add Successfully" : "Error";
            return Json(msg);
            
        }

        
    }
}
