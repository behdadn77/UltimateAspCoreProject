using Common.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class RegisterUserViewModel
    {
        [Display(Name = "آدرس ایمیل")]
        [Required(ErrorMessage = "آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "فرمت آدرس ایمیل صحیح نمیباشد")]
        public string EmailAddress { get; set; }

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

        [Display(Name = "نام")]
        //[Required(ErrorMessage = "نام را وارد کنید")]
        [StringLength(15, ErrorMessage = " نام باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        //[Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [StringLength(15, ErrorMessage = " نام خانوادگی باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
        public string LastName { get; set; }

        [DataAnnotation.Social.Telephone]
        [Display(Name = "شماره تلفن")]
        public string PhoneNum { get; set; }
    }
}
