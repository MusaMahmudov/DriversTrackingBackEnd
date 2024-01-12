using AbelloLLC.Core.Entities.Identity;
using AbelloLLC.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbelloLLC.DataAccess.Contexts
{
    public class AppDbContextInitializer
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppDbContextInitializer(AppDbContext context,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
        public async Task InitializerAsync()
        {
          await  _context.Database.MigrateAsync();   
        }


        public async Task UserSeedAsync()
        {
            foreach(var role in Enum.GetValues(typeof(Roles)))
            {
               await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString()});


            }

            var admin = new AppUser()
            {
                FullName = "Admin",
                Email = "turan.askerov2003@gmail.com",
                UserName = "Admin",
                IsActive = true,
            };

          await _userManager.CreateAsync(admin,"Salam123!");
         await   _userManager.AddToRoleAsync(admin,Roles.Admin.ToString());

        }
    }
}
