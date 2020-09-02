using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Identity;
using Data;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ViewModels.Administrator;

namespace UltimateAspCoreProject.Areas.Dashboard.Controllers
{
    [Authorize("AdministratorPolicy")]
    [Area("Dashboard")]
    [Route("Dashboard/[controller]/[action]")]
    public class AdminController : Controller
    {
        private readonly ApplicationContext db;
        private readonly CustomUserManager userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IOptions<IdentityDefaultOptions> options;
        private readonly ILogger<AdminController> logger;

        public AdminController(ApplicationContext db, CustomUserManager userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityDefaultOptions> options,
            ILogger<AdminController> logger)
        {
            this.db = db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.options = options;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string query = "")
        {
            ViewData["defaultAdminUserName"] = options.Value.AdminUser.EmailAddress;
            ViewData["defaultAdminRoleName"] = "Administrators";

            query = query ?? "";
            List<UserRolesViewModel> userRolesList = new List<UserRolesViewModel>();
            foreach (var user in userManager.Users.Where(x => x.UserName.Contains(query)).Skip((page - 1) * pageSize).Take(pageSize).OrderBy(x => x.UserName).ToList())
            {
                UserRolesViewModel userRoles = new UserRolesViewModel()
                {
                    User = user
                };
                foreach (var role in roleManager.Roles.ToList())
                {
                    userRoles.Roles.Add(new UserRoleStatus()
                    {
                        RoleName = role.Name,
                        IsInRole = await userManager.IsInRoleAsync(user, role.Name)
                    });
                }
                userRolesList.Add(userRoles);
            }
            ViewData["pageNum"] = page;
            ViewData["totalPages"] = Math.Ceiling(userManager.Users.Count() / (double)pageSize);
            ViewData["query"] = query;

            return View(userRolesList);
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            var user = await userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    Email = model.EmailAddress,
                    UserName = model.EmailAddress,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true
                };

                var status = await userManager.CreateAsync(user, model.Password);
                if (!status.Succeeded)
                {
                    TempData["GlobalError"] = $"Failed to register user {status.Errors.ToString()}";
                }
            }
            else
            {
                TempData["GlobalError"] = "Account already exists!";
            }
            TempData["GlobalSuccess"] = "User registerd successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return View(new EditUserViewModel()
            {
                CurrentEmailAddress = user.Email,
                EmailAddress = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            });
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(model.CurrentEmailAddress);
            if (user == null)
            {
                TempData["GlobalError"] = "User doesn't exist.";
                return RedirectToAction("Index");
            }
            if (model.EmailAddress != options.Value.AdminUser.EmailAddress && user.Email == options.Value.AdminUser.EmailAddress)
            {
                TempData["GlobalError"] = "Default Admin's Username can not be changed.";
                return RedirectToAction("Index");
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.EmailAddress;
            user.Email = model.EmailAddress;
            user.EmailConfirmed = true;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                TempData["GlobalError"] = $"Updating details was Unsuccessful. {result.Errors.ToString()}";
                return RedirectToAction("Index");
            }
            TempData["GlobalSuccess"] = "Details updated Successfully.";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult ChangePass(string username)
        {
            return View(new ChangePassViewModel()
            {
                EmailAddress = username
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePassViewModel model)
        {
            ApplicationUser user = await userManager.FindByNameAsync(model.EmailAddress);
            if (user == null)
            {
                TempData["GlobalError"] = "User doesn't exist.";
                return RedirectToAction("Index");
            }
            user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.NewPass);
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                TempData["GlobalError"] = $"Changing password was Unsuccessful.  {result.Errors.ToString()}";
                return RedirectToAction("Index");
            }
            TempData["GlobalSuccess"] = "Password changed Successfully.";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            return View(model: username);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserConfirm(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (await userManager.DeleteAsync(user) == IdentityResult.Success)
                {
                    TempData["GlobalSuccess"] = "User deleted successfully.";
                    return RedirectToAction("ManageUsers");
                }
                TempData["GlobalError"] = "Failed to delete user.";
                return RedirectToAction("ManageUsers");
            }
            TempData["GlobalError"] = "User not found.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserRoles(string username)
        {

            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                UserRolesViewModel userRoles = new UserRolesViewModel()
                {
                    User = user
                };
                foreach (var role in roleManager.Roles.ToList())
                {
                    userRoles.Roles.Add(new UserRoleStatus()
                    {
                        RoleName = role.Name,
                        IsInRole = await userManager.IsInRoleAsync(user, role.Name)
                    });
                }
                return View(userRoles);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserRoles(string username, List<string> roles)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                foreach (var role in roles) //note: Don't use lambda ForEach
                {
                    try
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                    catch (Exception)
                    {
                        TempData["GlobalError"] = $"Failed to assign user to {role}";
                        return RedirectToAction("ChangeUserRoles", routeValues: new { username });
                    }
                }
                var allRoles = roleManager.Roles.Select(x => x.Name).ToList();
                foreach (var role in allRoles.Except(roles))
                {
                    if (user.UserName == options.Value.AdminUser.EmailAddress && role == "Administrators")
                    {
                        TempData["GlobalError"] = "default site admin cannot be demoted";
                    }
                    else
                    {
                        try
                        {
                            await userManager.RemoveFromRoleAsync(user, role);
                        }
                        catch (Exception)
                        {
                            TempData["GlobalError"] = $"Failed to remove user from {role}";
                            return RedirectToAction("ChangeUserRoles", routeValues: new { username });
                        }
                    }
                }
            }
            else
            {
                TempData["GlobalError"] = "User Doesn't exist";
                return RedirectToAction("ChangeUserRoles", routeValues: new { username });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoveUserRoleOnCheckboxEvent(string username, string roleName, bool isInRole)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (isInRole)
                {
                    try
                    {
                        await userManager.AddToRoleAsync(user, roleName);
                    }
                    catch (Exception)
                    {
                        return Json($"Failed to assign user to {roleName}");
                    }
                }
                else
                {
                    if (username == options.Value.AdminUser.EmailAddress && roleName == "Administrators") //default site admin cannot be demoted
                    {
                        return Json("default site admin cannot be demoted");
                    }
                    else
                    {
                        try
                        {
                            await userManager.RemoveFromRoleAsync(user, roleName);
                        }
                        catch (Exception)
                        {
                            return Json($"Failed to remove user from {roleName}");
                        }
                    }
                }
                return Json("success");
            }
            return Json("User doesn't exist");
        }

    }
}
