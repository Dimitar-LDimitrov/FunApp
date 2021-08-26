namespace FunApp.Services.Models.Users
{
    using Microsoft.AspNetCore.Identity;

    public class UserBasicModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
