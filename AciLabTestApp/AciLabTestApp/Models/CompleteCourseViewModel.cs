using System.ComponentModel;

namespace AciLabTestApp.Models
{
    public class CompleteCourseViewModel
    {
        public int CompleteCourseId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        [DisplayName("Complete ? Incomplete")]
        public bool Status { get; set; }
    }
}