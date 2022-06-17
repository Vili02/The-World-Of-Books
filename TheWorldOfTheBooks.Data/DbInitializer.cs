using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorldOfTheBooks.Data.Models;

namespace TheWorldOfTheBooks.Data
{
    public class DbInitializer
    {
        public static void RoleSeeding(TheWorldOfBooksContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            context.Roles.AddRange(new IdentityRole()
            {
                Name = "User",
                NormalizedName = "User".ToUpper()
            },
            new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            });

            context.SaveChanges();
        }

        public static void AdminSeeding(UserManager<User> userManager)
        {
            string email = "pen@admin.bg";
            var user = userManager.FindByEmailAsync(email).GetAwaiter().GetResult();
            if (user == null)
            {
                User admin = new User()
                {
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper()
                };

                userManager.CreateAsync(admin, "Pen1@admin.bg").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            }

            if (userManager.GetRolesAsync(user).Result.Contains("Admin"))
            {
                return;
            }

            userManager.RemoveFromRoleAsync(user, "User").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }
    }
}
