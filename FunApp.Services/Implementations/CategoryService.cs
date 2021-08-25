namespace FunApp.Services.Implementations
{
    using Data;
    using Data.Models;
    using FunApp.Services.Models.Style;
    using Interfaces;
    using Models.Category;
    using System.Collections.Generic;
    using System.Linq;

    public class CategoryService : ICategoryService
    {
        private readonly FunAppDbContext db;

        public CategoryService(FunAppDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CategoryModel> All(int page = 1, int pageSize = 10)
        => this.db.Categories
                .OrderBy(c => c.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    ThumbnailImagePath = c.ThumbnailImagePath,
                    Title = c.Title
                })
                .ToList();

        public IEnumerable<BasicStyleModel> StylesByCategory(int categoryId)
        {
            var styles = this.db
                .Styles
                .Where(st => st.CategoryId == categoryId)
                .Select(st => new BasicStyleModel
                {
                    Id = st.Id,
                    Title = st.Title
                })
                .ToList();

            this.db.Add(styles);
            this.db.SaveChanges();

            return styles;
        }

        public void Create(string title, string thumbnailImagePath)
        {
            var category = new Category
            {
                Title = title,
                ThumbnailImagePath = thumbnailImagePath
            };

            this.db.Add(category);
            this.db.SaveChanges();
        }

        public void Edit(int id, string title, string thumbnailImagePath)
        {
            var existingCategory = this.db.Categories.Find(id);

            if (existingCategory == null)
            {
                return;
            }

            existingCategory.Title = title;
            existingCategory.ThumbnailImagePath = thumbnailImagePath;

            this.db.SaveChanges();
        }

        public void Delete(int? id)
        {
            if (id == null)
            {
                return;
            }

            var obj = this.db.Categories.Find(id);

            this.db.Remove(obj);
            this.db.SaveChanges();
        }

        public CategoryModel FindById(int id)
        {
            var customer = this.db
            .Categories
            .Where(c => c.Id == id)
            .Select(c => new CategoryModel
            {
                Id = id,
                Title = c.Title,
                ThumbnailImagePath = c.ThumbnailImagePath
            })
            .FirstOrDefault();

            return customer;
        }

        public bool CategoryExist(int id)
        {
            var exist = this.db.Categories.Find(id);

            if (exist == null)
            {
                return false;
            }

            return true;
        }

        public int Total() => this.db.Categories.Count();
    }
}
