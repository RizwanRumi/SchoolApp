using AciLabTestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AciLabTestApp.IBLL
{
    public interface ICourse
    {
        IList<CourseViewModel> GetAllCourses();
    }
}
