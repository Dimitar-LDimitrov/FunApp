namespace FunApp.Web.Controllers
{
    using Extentions;
    using Data.Models;
    using Models.User;
    using Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.AdministratorRole)]
    public class UsersController : Controller
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(IAdminUserService users,
                               UserManager<User> userManager,
                               RoleManager<IdentityRole> roles,
                               SignInManager<User> signInManager)
        {
            this.users = users;
            this.roleManager = roles;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost]
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
                return RedirectToAction(nameof(Index));
            }

            await this.userManager.AddToRoleAsync(user, modell.Role);

            return RedirectToAction(nameof(Index));
        }
    }
}
