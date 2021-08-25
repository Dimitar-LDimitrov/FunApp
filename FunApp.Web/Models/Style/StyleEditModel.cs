namespace FunApp.Web.Models.Style
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class StyleEditModel : StylesBasicModel
    {
        public bool IsEdit { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
