using Microsoft.AspNetCore.Identity;
using SaitynasLab.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaitynasLab.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Roles for testing
            await CreateAdmin();
            await CreateMusician();
            await CreateCreator();
        }

        private async Task CreateAdmin()
        {
            var newAdminUser = new IdentityUser
            {
                UserName = "admin",
                Email = "admin@admin.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createAdminUserResult = await _userManager.CreateAsync(newAdminUser, "Password1!");
                if (createAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRolesAsync(newAdminUser, UserRoles.All);
                }
            }
        }

        private async Task CreateMusician()
        {
            var newMusician = new IdentityUser
            {
                UserName = "musician",
                Email = "musician@musician.com"
            };

            var existingMusician = await _userManager.FindByNameAsync(newMusician.UserName);
            if (existingMusician == null)
            {
                var existingMusicianResult = await _userManager.CreateAsync(newMusician, "Password1!");
                if (existingMusicianResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newMusician, UserRoles.Musician);
                }
            }
        }

        private async Task CreateCreator()
        {
            var newCreator = new IdentityUser
            {
                UserName = "creator",
                Email = "creator@creator.com"
            };

            var existingCreator = await _userManager.FindByNameAsync(newCreator.UserName);
            if (existingCreator == null)
            {
                var existingCreatorResult = await _userManager.CreateAsync(newCreator, "Password1!");
                if (existingCreatorResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newCreator, UserRoles.Creator);
                }
            }
        }
    }
}
