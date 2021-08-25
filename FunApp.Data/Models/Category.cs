namespace FunApp.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        public string ThumbnailImagePath { get; set; }

        public IEnumerable<Style> Styles { get; set; } = new List<Style>();

        public IEnumerable<UserCategory> Users { get; set; } = new List<UserCategory>();
    }
}
