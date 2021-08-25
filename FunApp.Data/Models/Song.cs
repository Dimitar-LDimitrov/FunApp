namespace FunApp.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Song
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Artist { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double Duration { get; set; }

        [StringLength(300, MinimumLength = 1)]
        public string ThumbnailImage { get; set; }

        public string VideoLink { get; set; }

        public DateTime ReleaseDate { get; set; }

        public Style Style { get; set; }

        [NotMapped]
        public int StyleId { get; set; }

        [NotMapped]
        public int CategoryId {get; set;}
    }
}
