using AciLabTestApp.IBLL;
using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


        public bool AddCourse(CourseViewModel aCourse)
        {
            tblCourse tc = new tblCourse();
            tc.CourseId = aCourse.CourseId;
            tc.CourseCode = aCourse.CourseCode;
            tc.CourseName = aCourse.CourseName;

            try
            {
                dbContext.tblCourses.Add(tc);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
            
        }

        public CourseViewModel GetCourse(int id)
        {            
            tblCourse cs = dbContext.tblCourses.Find(id);

            CourseViewModel cvm = new CourseViewModel();
            if (cs != null)
            {                
                cvm.CourseId = cs.CourseId;
                cvm.CourseCode = cs.CourseCode;
                cvm.CourseName = cs.CourseName;
            }
           

            return cvm;
        }

        public bool EditCourse(CourseViewModel editCourse)
        {
            tblCourse tc = new tblCourse();
            tc.CourseId = editCourse.CourseId;
            tc.CourseCode = editCourse.CourseCode;
            tc.CourseName = editCourse.CourseName;
            try
            {
                dbContext.Entry(tc).State = EntityState.Modified;
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception  exp)
            {
                return false;
            }
           
        }
        
    }
}