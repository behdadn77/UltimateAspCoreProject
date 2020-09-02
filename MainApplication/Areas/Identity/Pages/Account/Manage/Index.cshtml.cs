using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Entities;
using Common.Identity;

namespace UltimateAspCoreProject.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly CustomUserManager _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            CustomUserManager userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "نام")]
            //[Required(ErrorMessage = "نام را وارد کنید")]
            [StringLength(15, ErrorMessage = " نام باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Display(Name = "نام خانوادگی")]
            //[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
            [StringLength(15, ErrorMessage = " نام خانوادگی باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
            public string LastName { get; set; }

            [DataAnnotation.Social.Phone]
            [Display(Name = "شماره تلفن")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var firstName = user.FirstName;
            var lastName = user.LastName;


            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.PhoneNumber = Input.PhoneNumber;
            var res = await _userManager.UpdateAsync(user);
            if (!res.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to update account details.";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
