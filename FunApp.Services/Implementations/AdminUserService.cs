namespace FunApp.Services.Implementations
{
    using Data;
    using Interfaces;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly FunAppDbContext db;

        public AdminUserService(FunAppDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AdminUserListingServiceModel> All()
        {
            var allAdmins = this.db
                .Users
                .OrderBy(u => u.UserName)
                .Select(u => new AdminUserListingServiceModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName
                })
                .ToList();

            return allAdmins;
        }

        public IEnumerable<UserBasicModel> AllListings(int page = 1, int pageSize = 10)
        => this.db.Users
                .OrderBy(u => u.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new UserBasicModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber
                })
                .ToList();

        public UserBasicModel ById(string id)
        => this.db
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserBasicModel
                {
                    Id = id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    Username = u.UserName
                })
                .FirstOrDefault();

        public int Total() => this.db.Users.Count();
    }
}
