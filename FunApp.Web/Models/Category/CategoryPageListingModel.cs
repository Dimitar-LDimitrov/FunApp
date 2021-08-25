namespace FunApp.Web.Models.Category
{
    using Services.Models.Category;
    using System.Collections.Generic;

    public class CategoryPageListingModel : BasicCategoryModel
    {
        public IEnumerable<Services.Models.Category.CategoryModel> Categories { get; set; }

        public int TotalPage { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage == 1
            ? 1
            : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPage
            ? this.TotalPage
            : this.CurrentPage + 1;
    }
}
