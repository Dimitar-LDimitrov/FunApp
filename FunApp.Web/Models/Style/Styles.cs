namespace FunApp.Web.Models.Style
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Services.Models.Style;
    using System.Collections.Generic;

    public class Styles
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<BasicStyleModel> AllStyles { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
