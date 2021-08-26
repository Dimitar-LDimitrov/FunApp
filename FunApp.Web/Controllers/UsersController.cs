namespace FunApp.Web.Controllers
{
    using Extentions;
    using Data;
    using Data.Models;
    using Models.User;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly FunAppDbContext db;

        public UsersController(IAdminUserService users,
                               UserManager<User> userManager,
                               RoleManager<IdentityRole> roles,
                               SignInManager<User> signInManager,
                               FunAppDbContext db)
        {
            this.users = users;
            this.roleManager = roles;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.LoginInValid = "true";

            if (ModelState.IsValid)
            {
                var result = await this.signInManager.PasswordSignInAsync(model.Email,
                                                                          model.Password,
                                                                          model.RememberMe,
                                                                          lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    model.LoginInValid = "";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            return PartialView("_UserLoginPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string retunrUrl = null)
        {
            await this.signInManager.SignOutAsync();

            if (retunrUrl == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return LocalRedirect(retunrUrl);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            model.RegistrationInValid = "true";

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await this.userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    model.RegistrationInValid = "";

                    await this.signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                AddErrorsToModelState(result);
            }

            return PartialView("_UserRegistrationPartial", model);
        }

        public IActionResult Index()
        {
            var allUsers = this.users.All();
            var allRoles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new AdminUsersViewModel
            {
                Users = allUsers,
                Roles = allRoles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddToRole(AddUserToRoleFormModel modell)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(modell.Role);
            var user = await this.userManager.FindByIdAsync(modell.UserId);
            var userExists = user != null;

            if (!roleExists || !userExists)
            {
                ModelState.AddModelError(string.Empty, "Invalid Identity details.");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(AllListing));
            }

            await this.userManager.AddToRoleAsync(user, modell.Role);

            TempData.AddSuccessMessage($"User {user.UserName} successfully added to the {modell.Role} role.");

            return RedirectToAction(nameof(AllListing));
        }

        [AllowAnonymous]
        public async Task<bool> UserNameExists(string userName)
        {
            bool userNameExists = await this.db.Users
                .AnyAsync(u => u.UserName.ToUpper() == userName.ToUpper());

            if (userNameExists)
            {
                return true;
            }
            return false;
        }

        private void AddErrorsToModelState(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
        }

        public IActionResult AllListing(int page = 1, int pageSize = 15)
        {
            var allUsers = this.users.AllListings(page, pageSize);
            var allRoles = this.roleManager
                .Roles
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
                .ToList();

            return View(new UsersPageListingModel
            {
                CurrentPage = page,
                TotalPage = (int)Math.Ceiling(this.users.Total() / (double)pageSize),
                AllUsers = allUsers,
                Roles = allRoles
            });
        }
    }
}
