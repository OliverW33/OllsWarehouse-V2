using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OllsWarehouse.Areas.Identity.Data;
using OllsWarehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OllsWarehouse.Controllers
{
    public class SecurityController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public SecurityController(UserManager<ApplicationUser> newUserManager, RoleManager<ApplicationRole> newRoleManager,
            SignInManager<ApplicationUser> newSignInManager)
        {
            userManager = newUserManager;
            roleManager = newRoleManager;
            signInManager = newSignInManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUp newUser)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("Manager").Result)
                {
                    ApplicationRole newRole = new ApplicationRole();
                    newRole.Name = "Manager";
                    IdentityResult roleFound = roleManager.CreateAsync(newRole).Result;
                }
            }

            ApplicationUser registeredUser = new ApplicationUser();

            registeredUser.firstName = newUser.firstName;
            registeredUser.lastName = newUser.lastName;
            registeredUser.UserName = newUser.userName;
            registeredUser.Email = newUser.userEmail;

            IdentityResult userResult = userManager.CreateAsync(registeredUser, newUser.userPassword).Result;

            if (userResult.Succeeded)
            {
                userManager.AddToRoleAsync(registeredUser, "Manager").Wait();
                return RedirectToAction("SignIn", "Security");
            }
            else
            {
                ModelState.AddModelError("", "Invalid User Details, Please try again");
            }

            return View(newUser);

        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(SignIn user)
        {
            if (ModelState.IsValid)
            {
                var authorizeResult = signInManager.PasswordSignInAsync(user.userName, user.userPassword, user.rememberMe, false).Result;

                if (authorizeResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Details, Please try again");
                }
            }

            return View(user);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult SignOut()
        {
            signInManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
