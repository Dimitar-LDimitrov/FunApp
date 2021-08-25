namespace FunApp.Services.Interfaces
{
    using FunApp.Services.Models.Style;
    using Models.Category;
    using System.Collections.Generic;

    public interface ICategoryService
    {
        IEnumerable<CategoryModel> All(int page = 1, int pageSize = 10);

        IEnumerable<BasicStyleModel> StylesByCategory(int categoryId);

        void Create(string title, string thumbnailImagePath);

        void Edit(int id, string title, string thumbnailImagePath);

        void Delete(int? id);

        CategoryModel FindById(int id);

        int Total();

        bool CategoryExist(int id);
    }
}
