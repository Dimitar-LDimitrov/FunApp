namespace FunApp.Web.Areas.Admin.Models.Category
{
    using Services.Models.Category;
    using System.Collections.Generic;

    public class CategoryModel : BasicCategoryModel
    {
        public IEnumerable<CategoryModel> Categories { get; set; }
    }
}
