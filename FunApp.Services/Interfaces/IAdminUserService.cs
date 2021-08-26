namespace FunApp.Services.Interfaces
{
    using Models.Users;
    using System.Collections.Generic;

    public interface IAdminUserService
    {
        IEnumerable<AdminUserListingServiceModel> All();

        IEnumerable<UserBasicModel> AllListings(int page = 1, int pageSize = 10);

        UserBasicModel ById(string id);

        int Total();
    }
}
