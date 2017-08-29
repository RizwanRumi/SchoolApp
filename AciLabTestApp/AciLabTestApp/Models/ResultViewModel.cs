using System.ComponentModel.DataAnnotations;

namespace AciLabTestApp.Models
{
    public class ResultViewModel
    {
        public int ResultId { get; set; }
        [Required]
        public int StudentId { get; set; }
        [Required]
        public string SemesterName { get; set; }
        [Required]
        public string Grade { get; set; }
    }
}