using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AciLabTestApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [Required]
        public string CourseName { get; set; }
    }
}