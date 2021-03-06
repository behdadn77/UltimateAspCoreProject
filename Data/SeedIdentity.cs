﻿using Common.Identity;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class SeedIdentity
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IdentityDefaultOptions identityProperties)
        {
            var userManager = serviceProvider.GetRequiredService<CustomUserManager>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "Administrators", "ContentManagers" };
            foreach (var item in roles)
            {
                if (await roleManager.RoleExistsAsync(item) == false)
                {
                    IdentityRole newrole = new IdentityRole(item);
                    await roleManager.CreateAsync(newrole);
                }
            }

            var user = await userManager.FindByEmailAsync(identityProperties.AdminUser.EmailAddress);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    Email = identityProperties.AdminUser.EmailAddress,
                    NormalizedEmail = identityProperties.AdminUser.EmailAddress.ToUpper(),
                    UserName = identityProperties.AdminUser.EmailAddress,
                    NormalizedUserName = identityProperties.AdminUser.EmailAddress.ToUpper(), //IMPORTENT USERNAME MUST BE SAME AS EMAIL ADDRESS OTHERWISE LOGIN FAILES
                    FirstName="Admin",
                    EmailConfirmed = true,
                };


                var status = await userManager.CreateAsync(user, identityProperties.AdminUser.Password);
                if (status.Succeeded == true)
                {
                    await userManager.AddToRoleAsync(user, "Administrators");
                }
            }
        }

    }
}
