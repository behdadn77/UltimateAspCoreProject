using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels.Administrator
{
    public class RegisterUserViewModel
    {
        [DataAnnotation.Social.EmailAddress(isRequired: true)]
        public string EmailAddress { get; set; }

        [DataAnnotation.User.Password(isRequired: true)]
        public string Password { get; set; }

        [DataAnnotation.User.ConfirmPassword("Password")]
        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
