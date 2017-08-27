using AciLabTestApp.IBLL;
using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AciLabTestApp.BL
{
    public class CourseManager : ICourse
    {
        StudentDBEntities dbContext = new StudentDBEntities();

        public IList<CourseViewModel> GetAllCourses()
        {
            IList<CourseViewModel> courses = null;
            
            courses = dbContext.tblCourses.Select(
                s => new CourseViewModel()
                {
                    CourseId = s.CourseId,
                    CourseCode = s.CourseCode,
                    CourseName = s.CourseName
                }).ToList();
            
            return courses;
        }
    }
}