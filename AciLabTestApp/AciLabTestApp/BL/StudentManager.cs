using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AciLabTestApp.IBLL;
using AciLabTestApp.Models;

namespace AciLabTestApp.BL
{
    public class StudentManager : IStudent
    {
        StudentDBEntities dbContext = new StudentDBEntities();
        public IList<StudentViewModel> GetAllStudent()
        {
            IList<StudentViewModel> students = null;

            students = dbContext.tblStudents.Select(
                s => new StudentViewModel()
                {
                    StudentId =  s.StudentId,
                    Name = s.Name,
                    Email = s.Email,
                    Address =  s.Address,
                    Department =  s.Department,
                    Batch =  s.Batch,
                    Enrolled =  s.Enrolled,
                    Password =  s.Password
                }).ToList();

            return students;
        }

        public StudentViewModel AddNewStudent(StudentViewModel model)
        {
            tblStudent astudent = new tblStudent();
            astudent.StudentId = model.StudentId;
            astudent.Name = model.Name;
            astudent.Email = model.Email;
            astudent.Address = model.Address;
            astudent.Department = model.Department;
            astudent.Batch = model.Batch;
            astudent.Enrolled = model.Enrolled;
            astudent.Password = model.Password;

            dbContext.tblStudents.Add(astudent);
            dbContext.SaveChanges();

            model.StudentId = astudent.StudentId;
            model.Name = astudent.Name;
            model.Email = astudent.Email;
            model.Password = astudent.Password;


            return model;
        }

        public IList<TutorialViewModel> GetAllTutorial(int id)
        {
            IList<TutorialViewModel> tutorials = null;

            tutorials = dbContext.tblTutorials.Select(
                s => new TutorialViewModel()
                {
                    TutorialId =  s.TutorialId,
                    StudentId = s.StudentId,
                    FileName =  s.FileName,
                    Complete = s.Complete
                }).Where(s=>s.StudentId == id).ToList();

            return tutorials;
        }


        public bool AddTutotrial(TutorialViewModel tmodel)
        {
            tblTutorial aTutorial = new tblTutorial();
            aTutorial.StudentId = tmodel.StudentId;
            aTutorial.TutorialId = tmodel.TutorialId;
            aTutorial.FileName = tmodel.FileName;
            aTutorial.Complete = tmodel.Complete;
          

            dbContext.tblTutorials.Add(aTutorial);
            dbContext.SaveChanges();
            
            return true;
        }
    }
}