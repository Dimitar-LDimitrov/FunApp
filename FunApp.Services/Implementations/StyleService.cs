namespace FunApp.Services.Implementations
{
    using Data;
    using Data.Models;
    using FunApp.Services.Models.Style;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class StyleService : IStyleService
    {
        private readonly FunAppDbContext db;

        public StyleService(FunAppDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<BasicStyleModel> ById(int categoryId)
        {
            return this.db.Styles
                .Where(st => st.CategoryId == categoryId)
                .Select(c => new BasicStyleModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    CategoryId = categoryId
                })
                .ToList();
        }

        public IEnumerable<BasicStyleModel> All()
        => this.db
            .Styles
            .Select(st => new BasicStyleModel
            {
                Id = st.Id,
                Title = st.Title,
                CategoryId = st.CategoryId
            })
            .ToList();

        public void Create(int id, string title, int categoryId)
        {
            var style = new Style
            {
                Id = id,
                Title = title,
                CategoryId = categoryId
            };

            this.db.Styles.Add(style);
            this.db.SaveChanges();
        }

        public void Edit(int id, string title)
        {
            var existStyle = this.db.Styles.Find(id);

            if (existStyle == null)
            {
                return;
            }

            existStyle.Title = title;

            this.db.SaveChanges();
        }

        public BasicStyleModel FindById(int id)
        {
            return this.db
                .Styles
                .Where(st => st.Id == id)
                .Select(st => new StyleListingModel
                {
                    Id = id,
                    Title = st.Title,
                    CategoryId = st.CategoryId,
                    
                })
                .FirstOrDefault();
        }

        public void Delete(int id)
        {
            var itemToDelete = this.db
                 .Styles
                 .Where(st => st.Id == id)
                 .FirstOrDefault();

            if (itemToDelete == null)
            {
                return;
            }

            this.db.Remove(itemToDelete);
            this.db.SaveChanges();
        }
    }
}
