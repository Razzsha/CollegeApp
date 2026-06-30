using CollegeApp.Models.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        [ValidateNever]
        public int id { get; set; }

        //[Required (ErrorMessage = "Student name is required")]
        //[StringLength(30)]

        public string StudentName { get; set; }

        [EmailAddress (ErrorMessage = " Email Address must be Valid")]

        public string Email { get; set; }
        [Range(10, 20)]
        public int Age { get; set; }
        [Required]

        public string Address { get; set; }
        //[DateCheck]
        public DateTime DOB { get; set;}

        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfimPassword { get; set; }
    }
}
