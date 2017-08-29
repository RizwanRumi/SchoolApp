using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AciLabTestApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        
        [DisplayName("Course Code")]
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "{0}'s length should be between {2} and {1}.")]
        public string CourseCode { get; set; }

        [DisplayName("Course Name")]
        [Required(ErrorMessage = "{0} is required.")]
        public string CourseName { get; set; }
    }
}