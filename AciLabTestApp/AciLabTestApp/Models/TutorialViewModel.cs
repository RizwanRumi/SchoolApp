using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AciLabTestApp.Models
{
    public class TutorialViewModel
    {
        public int  TutorialId { get; set; }
        public int StudentId { get; set; }
        public string FileName { get; set; }
        public bool Complete { get; set; }
    }
}