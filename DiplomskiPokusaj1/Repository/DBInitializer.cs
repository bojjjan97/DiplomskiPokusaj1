using DiplomskiPokusaj1.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomskiPokusaj1.Repository
{
    public class DBInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }
        public static void SeedUsers(UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync("SuperAdmin").Result == null)
            {
                User user = new User();
                user.UserName = "SuperAdmin";
                user.Email = "bogdan@singularity.is";
                user.Firstname = "Super";
                user.Lastname = "Admin";
                user.CreatedAt = DateTime.Now;

                IdentityResult result = userManager.CreateAsync(user, "asdd912ijd21i@!@9229!4;1cxJsdasaAdasdasSAD@").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Librarian").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Librarian";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
