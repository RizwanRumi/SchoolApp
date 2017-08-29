using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AciLabTestApp.Models
{
    public class StudentViewModel
    {
        public int StudentId { get; set; }

        [DisplayName("Student Name")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Name { get; set; }

        [DisplayName("Student Email")]
        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Address { get; set; }

        [DisplayName("Department")]
        [Required(ErrorMessage = "{0} is required.")]
        public string Department { get; set; }

        [Required]
        public int Batch { get; set; }
        [Required]
        public DateTime Enrolled { get; set; }

        [Required]
        public string Password { get; set; }
    }
}