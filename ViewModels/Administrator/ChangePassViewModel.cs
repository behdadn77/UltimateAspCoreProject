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
        [StringLength(30, ErrorMessage = " رمز عبور باید حداقل" + " {2}" + "و حداکثر" +"{1}"+"کارکتر باشد", MinimumLength =8)]
        [RegularExpression(
            "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{1,}$",
            ErrorMessage = "رمز عبور باید شامل حداقل یک حرف بزرگ، یک حرف کوچک، یک رقم و یک کارکتر ویژه (مانند !@#$%^&*) باشد")]
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
