using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Common.Identity;
using Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using reCAPTCHA.AspNetCore;

namespace Dashboard.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CustomUserManager _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IRecaptchaService recaptchaService;

        public RegisterModel(
            CustomUserManager userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IRecaptchaService recaptchaService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            this.recaptchaService = recaptchaService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Display(Name = "نام")]
            [Required(ErrorMessage = "نام را وارد کنید")]
            [StringLength(15, ErrorMessage = " نام باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
            public string FirstName { get; set; }

            [Display(Name = "نام خانوادگی")]
            [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
            [StringLength(15, ErrorMessage = " نام خانوادگی باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
            public string LastName { get; set; }

            [Display(Name = "آدرس ایمیل")]
            [Required(ErrorMessage = "آدرس ایمیل را وارد کنید")]
            [EmailAddress(ErrorMessage = "فرمت آدرس ایمیل صحیح نمیباشد")]
            public string Email { get; set; }

            [Display(Name = "رمز عبور")]
            [Required(ErrorMessage = "رمز عبور را وارد کنید")]
            [StringLength(30, ErrorMessage = " رمز عبور باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 8)]
            [RegularExpression(
           "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{1,}$",
           ErrorMessage = "رمز عبور باید شامل حداقل یک حرف بزرگ، یک حرف کوچک، یک رقم و یک کارکتر ویژه (مانند !@#$%^&*) باشد")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "تکرار رمز عبور")]
            [Required(ErrorMessage = "تکرار رمز عبور را وارد کنید")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نمیباشد")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            var recaptcha = await recaptchaService.Validate(Request);
            if (!recaptcha.success || recaptcha.score != 0 && recaptcha.score < 5)
                ModelState.AddModelError("Recaptcha", "reCAPTCHA failed!");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "آدرس ایمیل خود را تائید کنید",
                        $"برای تایید آدرس ایمیل خود  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>اینجا را کلیک کنید</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
