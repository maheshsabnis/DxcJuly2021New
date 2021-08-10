using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_App_Security.Controllers
{
    public class RoleUserManagerController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleUserManagerController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var ur = new UserRole();
            ViewBag.Users = await userManager.Users.ToListAsync();
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            return View(ur);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRole ur)
        {
            // Search USer NAme Based on User Id
            var user = await userManager.FindByIdAsync(ur.UserName);

            // Search Role Name BAsed on Role Id
            var role = await roleManager.FindByIdAsync(ur.Name);

            // Logic TO Assign Role to USer
              await userManager.AddToRoleAsync(user,role.Name);

            ViewBag.Message = $"User {user.UserName} is Assigned to Role {role.Name}";
            ViewBag.Users = await userManager.Users.ToListAsync();
            ViewBag.Roles = await roleManager.Roles.ToListAsync();
            return View("Index");
        }
    }

    public class UserRole
    {
        public string UserName { get; set; }
        public string Name { get; set; }
    }
}
