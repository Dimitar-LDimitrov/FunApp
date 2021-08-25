namespace FunApp.Services.Models.Style
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class StyleListingModel : BasicStyleModel
    {
        public IEnumerable<SelectListItem> Categories { get; set; } 
    }
}
