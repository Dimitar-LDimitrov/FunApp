namespace FunApp.Web.Extentions
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;

    public static class ConvertExtention
    {
        public static List<SelectListItem> ConverToSelectList<T>(this IEnumerable<T> collection, int selectedValue) where T : IPrimaryProperties
        {
            return (from item in collection
                    select new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString(),
                        Selected = (item.Id == selectedValue)
                    }).ToList(); 
        }
    }
}
