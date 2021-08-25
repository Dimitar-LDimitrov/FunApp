namespace FunApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Joke
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(3000)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Likes { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
