using System.Collections.Generic;
using System.Linq;
using AciLabTestApp.IBLL;
using AciLabTestApp.Models;

namespace AciLabTestApp.BL
{
    public class StudentManager : IStudent
    {
        private StudentDBEntities dbContext = new StudentDBEntities();

        public IList<StudentViewModel> GetAllStudent()
        {
            IList<StudentViewModel> students = dbContext.tblStudents.Select(
                s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Name = s.Name,
                    Email = s.Email,
                    Address = s.Address,
                    Department = s.Department,
                    Batch = s.Batch,
                    Enrolled = s.Enrolled,
                    Password = s.Password
                }).ToList();

            return students;
        }

        public StudentViewModel AddNewStudent(StudentViewModel model)
        {
            var astudent = new tblStudent
            {
                StudentId = model.StudentId,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                Department = model.Department,
                Batch = model.Batch,
                Enrolled = model.Enrolled,
                Password = model.Password
            };

            dbContext.tblStudents.Add(astudent);
            dbContext.SaveChanges();

            model.StudentId = astudent.StudentId;
            model.Name = astudent.Name;
            model.Email = astudent.Email;
            model.Password = astudent.Password;

            return model;
        }

        public IList<TutorialViewModel> GetAllTutorial(int stdId)
        {
            IList<TutorialViewModel> tutorials = dbContext.tblTutorials.Select(
                s => new TutorialViewModel
                {
                    TutorialId = s.TutorialId,
                    StudentId = s.StudentId,
                    FileName = s.FileName,
                    Complete = s.Complete
                }).Where(s => s.StudentId == stdId).ToList();

            return tutorials;
        }


        public bool AddTutotrial(TutorialViewModel tmodel)
        {
            var aTutorial = new tblTutorial
            {
                StudentId = tmodel.StudentId,
                TutorialId = tmodel.TutorialId,
                FileName = tmodel.FileName,
                Complete = tmodel.Complete
            };

            dbContext.tblTutorials.Add(aTutorial);
            dbContext.SaveChanges();

            return true;
        }

        public IList<CompleteCourseViewModel> GetAllCompletedCourse(int stdId)
        {
            var cmpCourseList = new List<CompleteCourseViewModel>();
            var completeCourse = dbContext.tblCourseCompletes.Where(s => s.StudentId == stdId).ToList();
            var aCourseManager = new CourseManager();
            var courseList = aCourseManager.GetAllCourses();

            foreach (var item in completeCourse)
            {
                cmpCourseList.AddRange(from model in courseList
                    where item.CourseId == model.CourseId
                    select new CompleteCourseViewModel
                    {
                        CourseId = model.CourseId, CourseName = model.CourseName, Status = item.Status, StudentId = item.StudentId, CompleteCourseId = item.Id
                    });
            }
            
            return cmpCourseList;
        }

        public IList<CourseViewModel> GetRemainCourseList(int stdId)
        {
            IList<CourseViewModel> remaincourseList;

            var completeCourse = dbContext.tblCourseCompletes.Select(
                cmp => new CompleteCourseViewModel
                {
                    CourseId  = cmp.CourseId,
                    StudentId = cmp.StudentId
                }).Where(cmp => cmp.StudentId == stdId).ToList();

            
                var aCourseManager = new CourseManager();

                var courseList = aCourseManager.GetAllCourses();

            if(completeCourse.Count == 0)
            {
                remaincourseList = courseList;
            }
            else
            {
                foreach (var model in from model in courseList.ToList() from cmpModel in completeCourse.Where(cmpModel => cmpModel.CourseId == model.CourseId) select model)
                {
                    courseList.Remove(model);
                }

                remaincourseList = courseList ;
            }

            return remaincourseList;
        }

        public bool AddCompleteCourseViewModel(CompleteCourseViewModel cmpModel)
        {
            var cmpCourse = new tblCourseComplete
            {
                CourseId = cmpModel.CourseId,
                StudentId = cmpModel.StudentId,
                Status = cmpModel.Status 
            };

            dbContext.tblCourseCompletes.Add(cmpCourse);
            dbContext.SaveChanges();

            return true;
        }

        public List<ResultViewModel> GetAllResult(int stdId)
        {
            List<ResultViewModel> students = dbContext.tblResults.Select(
                s => new ResultViewModel()
                {
                    ResultId =  s.ResId,
                    StudentId = s.StudentId,
                    SemesterName = s.SemesterName,
                    Grade = s.Grade
                }).ToList();

            return students;
        } 
    }
}