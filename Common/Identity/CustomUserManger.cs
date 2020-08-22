using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Common.Identity
{
    public class CustomUserManager : UserManager<ApplicationUser>
    {
        public CustomUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<CustomUserManager> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            PasswordHasher = new CustomPasswordHasher();
        }

        public async Task<string> GetUserFullNameAsyncAsync(ClaimsPrincipal principal)
        {
            var user =await base.GetUserAsync(principal);
            return $"{user.FirstName} {user.LastName}";
        }
    }

    public class CustomPasswordHasher : PasswordHasher<ApplicationUser>
    {
        private string EncryptPassword(string password)
        {
            byte[] b = System.Text.Encoding.UTF8.GetBytes(password);
            var hashing = SHA512.Create();
            b = hashing.ComputeHash(b);
            return "CustomHashedPass" + Convert.ToBase64String(b);
        }


        public override string HashPassword(ApplicationUser user, string password)
        {
            return EncryptPassword(password);
        }

        public override PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == EncryptPassword(providedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }

    }


}
