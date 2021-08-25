namespace FunApp.Web.Areas.Admin.Models.Styles
{
    using Data.Models;
    using Services.Models.Style;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StyleFormModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 3)]
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<StyleListingModel> Styles { get; set; }
    }
}
