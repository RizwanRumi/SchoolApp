using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AciLabTestApp.Models;

namespace AciLabTestApp.IBLL
{
    public interface IStudent
    {
        IList<StudentViewModel> GetAllStudent();
        StudentViewModel AddNewStudent(StudentViewModel model);
        IList<TutorialViewModel> GetAllTutorial(int stdId);
        bool AddTutotrial(TutorialViewModel tmodel);
        IList<CompleteCourseViewModel> GetAllCompletedCourse(int stdId);
        IList<CourseViewModel> GetRemainCourseList(int stdId);

        bool AddCompleteCourseViewModel(CompleteCourseViewModel cmpModel);

    }
}
