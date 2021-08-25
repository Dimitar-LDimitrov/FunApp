namespace FunApp.Services.Interfaces
{
    using FunApp.Data.Models;
    using Models.Style;
    using System.Collections.Generic;

    public interface IStyleService
    {
        IEnumerable<BasicStyleModel> All();

        IEnumerable<BasicStyleModel> ById(int categoryId);

        void Create(int id, string title, int categoryId);

        void Edit(int id, string title);

        void Delete(int id);

        BasicStyleModel FindById(int id);
    }
}
