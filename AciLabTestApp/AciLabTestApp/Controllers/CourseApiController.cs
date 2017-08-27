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
        
    }
}
