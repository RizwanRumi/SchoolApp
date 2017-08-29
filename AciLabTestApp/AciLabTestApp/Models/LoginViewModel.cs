using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AciLabTestApp.Models
{
    public class LoginViewModel
    {
        public int LoginId { get; set; }
        public int StudentId { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [DataType(DataType.EmailAddress)]
        public String StudentEmail { get; set; }
        [Required]
        public String Password { get; set; }
       
    }
}