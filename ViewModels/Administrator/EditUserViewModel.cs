using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class EditUserViewModel
    {
        [Required]
        [EmailAddress]
        public string CurrentEmailAddress { get; set; }

        [Display(Name ="آدرس ایمیل")]
        [Required(ErrorMessage ="آدرس ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage ="فرمت آدرس ایمیل صحیح نمیباشد")]
        public string EmailAddress { get; set; }

        [Display(Name ="نام")]
        [Required(ErrorMessage = "نام را وارد کنید")]
        [StringLength(15, ErrorMessage = " نام باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
        public string FirstName { get; set; }

        [Display(Name ="نام خانوادگی")]
        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [StringLength(15, ErrorMessage = " نام خانوادگی باید حداقل" + " {2}" + "و حداکثر" + "{1}" + "کارکتر باشد", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}
