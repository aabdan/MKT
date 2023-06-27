using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MKT.Website.UI.Models;
using MKT.Website.UI.ViewModels;
using System.Data;

namespace MKT.Website.UI.Controllers
{
    public class AccountController : Controller
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterVM registerVM)

        {
            if (ModelState.IsValid)
            {

                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.UserName = registerVM.UserName;
                applicationUser.PasswordHash = registerVM.Password;
                applicationUser.Email = registerVM.Email;
                IdentityResult result = await _userManager.CreateAsync(applicationUser, registerVM.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(applicationUser, "Admin");
                    await _signInManager.SignInAsync(applicationUser, false);
                    return RedirectToAction("Index", "Company");
                }
                else
                {
                    foreach (var item in result.Errors)

                    { ModelState.AddModelError("", item.Description); }

                }

            }
            return View(registerVM);

        }


        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (!Request.Headers["X-Requested-With"].Equals("XMLHttpRequest"))
            {
                return RedirectToAction("Main", "Home");
            }

            return PartialView("LoginPartial");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {

            if (ModelState.IsValid)
            {
                ApplicationUser admin = await _userManager.FindByNameAsync(loginVM.UserName);
               
                if (admin != null)
                {

                  var result=  await _signInManager.PasswordSignInAsync(admin, loginVM.Password, loginVM.RememberMe, false);

                    if(result.Succeeded)
                        return Json(new { success = true });

                }
               
                  ModelState.AddModelError("", "Username or Password Wrong");

            }

            //return PartialView("LoginPartial", loginVM);
             return Json(new { success = false });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Main", "Home");
       
        }


    }
}
