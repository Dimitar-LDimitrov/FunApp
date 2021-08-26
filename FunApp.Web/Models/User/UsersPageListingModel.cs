namespace FunApp.Web.Models.User
{
    using Services.Models.Users;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class UsersPageListingModel
    {
        public IEnumerable<UserBasicModel> AllUsers { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

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
