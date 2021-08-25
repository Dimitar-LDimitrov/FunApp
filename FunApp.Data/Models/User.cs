namespace FunApp.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [StringLength(300, MinimumLength = 5)]
        public string Address { get; set; }

        public virtual IEnumerable<UserCategory> Categories { get; set; } = new List<UserCategory>();

        public virtual IEnumerable<UserJokes> Jokes { get; set; } = new List<UserJokes>();
    }
}
