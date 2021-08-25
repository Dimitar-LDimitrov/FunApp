namespace FunApp.Web.Models.User
{
    using System.ComponentModel.DataAnnotations;

    public class AddUserToRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
