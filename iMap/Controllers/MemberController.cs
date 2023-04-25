using iMap.Models;
using iMap.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;
using Aspose.Email.Clients.Activity;
using GoogleMaps.LocationServices;
using iMap.Data;

namespace iMap.Controllers
{
    [Authorize]
    public class MemberController : BaseController
    {


        public MemberController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager, AppIdentityDbContext context) : base(userManager, signInManager, roleManager, context)
        {

        }
        
        [Authorize]
        public IActionResult Index()
        {

            AppUser user = CurrentUser;
            UserViewModel userViewModel = user.Adapt<UserViewModel>();


            return View(userViewModel);
        }
        [Authorize]
        public IActionResult PasswordChange()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult PasswordChange(PasswordChangeViewModel passwordChangeViewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

                bool exist = userManager.CheckPasswordAsync(user, passwordChangeViewModel.PasswordOld).Result;

                if (exist)
                {
                    IdentityResult result = userManager.ChangePasswordAsync(user, passwordChangeViewModel.PasswordOld, passwordChangeViewModel.PasswordNew).Result;

                    if (result.Succeeded)
                    {
                        userManager.UpdateSecurityStampAsync(user);

                        signInManager.SignOutAsync();
                        signInManager.PasswordSignInAsync(user, passwordChangeViewModel.PasswordNew, true, false);

                        ViewBag.success = "true";
                    }
                    else
                    {
                        AddModelError(result);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Your old password is wrong.");
                }

            }
            return View(passwordChangeViewModel);
        }

        [Authorize]
        public RedirectToActionResult LogOut()
        {
            return RedirectToAction("Login", "Home");
        }
        

        

        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ChatPopupView()
        {
            return View();
        }

       
    }
}
