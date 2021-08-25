namespace FunApp.Services.Implementations
{
    using Data;
    using Interfaces;
    using Models.Users;
    using System.Collections.Generic;
    using System.Linq;

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
    }
}
