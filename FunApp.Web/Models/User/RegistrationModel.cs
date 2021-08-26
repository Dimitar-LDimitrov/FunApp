namespace FunApp.Web.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class RegistrationModel
    {
        [Required]
        [EmailAddress]
        [StringLength(30, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 5)]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } 

        public bool AcceptUserAgreement { get; set; }

        public string RegistrationInValid { get; set; }
    }
}
