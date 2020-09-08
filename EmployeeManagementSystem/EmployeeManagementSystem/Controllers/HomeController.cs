using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementSystem.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public HomeController(ILogger<HomeController> logger,AppDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }


        public async Task<IActionResult> Index() 
        {
            IdentityRole iR1 = new IdentityRole
            {
                Name = "Admin"
            };
            IdentityResult roleResult1 = await roleManager.CreateAsync(iR1);
            foreach (IdentityError error in roleResult1.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            IdentityRole iR2 = new IdentityRole
            {
                Name = "HR"
            };
            IdentityResult roleResult2 = await roleManager.CreateAsync(iR2);
            foreach (IdentityError error in roleResult2.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            IdentityRole iR3 = new IdentityRole
            {
                Name = "Employee"
            };
            IdentityResult roleResult3 = await roleManager.CreateAsync(iR3);
           
            foreach (IdentityError error in roleResult3.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }



            var adminName = "tash@gmail.com";
            var adminEmail = "tash@gmail.com";
            var adminPassword = "Tash@123";
            var user = new IdentityUser
            {
                UserName = adminName,
                Email = adminEmail
            };
            var result = await userManager.CreateAsync(user, adminPassword);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("Index");
            }


            var hrName = "taif@gmail.com";
            var hrEmail = "taif@gmail.com";
            var hrPassword = "Taif@123";
            var user2 = new IdentityUser
            {
                UserName = hrName,
                Email = hrEmail
            };
            var result2 = await userManager.CreateAsync(user2, hrPassword);

            if (result2.Succeeded)
            {
                await userManager.AddToRoleAsync(user2, "HR");
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View();

        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
