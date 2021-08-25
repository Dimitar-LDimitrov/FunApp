namespace FunApp.Services.Interfaces
{
    using Models.Users;
    using System.Collections.Generic;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();
    }
}
