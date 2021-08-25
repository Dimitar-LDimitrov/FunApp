namespace FunApp.Data.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Style
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        
        //Gives to Style Form Model dropdown list of categories
        [NotMapped]
        public virtual ICollection<SelectListItem> Categories { get; set; } 

        public ICollection<Song> Songs { get; set; } = new List<Song>();

        [NotMapped]
        public int SongId { get; set; }
    }
}
