using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Identity
{
    public class IdentityDefaultOptions
    {
        public AdminUser AdminUser { get; set; }
    }
    public class AdminUser
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
    public class PasswordRequirements
    {
        public string Regex { get; set; }
        public int MinLenght { get; set; }
        public int MaxLenght { get; set; }
        public bool AllowWhiteSpace { get; set; }
        public int RequiredDigits { get; set; }
        public int RequiredUppercase { get; set; }
        public int RequiredLower { get; set; }
        public int RequiredSpecialChar { get; set; }
    }
}
