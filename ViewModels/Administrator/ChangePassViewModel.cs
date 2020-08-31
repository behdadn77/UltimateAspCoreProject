using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class ChangePassViewModel
    {
        [Display(Name = "رمز عبور جدید")]
        [Required(ErrorMessage = "رمز عبور را وارد کنید")]
        [StringLength(255, ErrorMessage = " رمز عبور باید حداقل" + " {2}" + "و حداکثر" +"{1}"+"کارکتر باشد", MinimumLength =5)]
        [RegularExpression(
            "^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }

        [Display(Name ="تکرار رمز عبور جدید")]
        [Required(ErrorMessage="تکرار رمز عبور را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("NewPass", ErrorMessage = "رمز عبور و تکرار آن یکسان نمیباشد")]
        public string ConfirmNewPass { get; set; }

        [Required]
        [EmailAddress]
        public string  EmailAddress { get; set; }
    }
}
