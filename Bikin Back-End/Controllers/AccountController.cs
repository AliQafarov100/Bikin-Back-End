using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bikin_Back_End.Models;
using Bikin_Back_End.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bikin_Back_End.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _signIn;

        public AccountController(UserManager<AppUser> manager,SignInManager<AppUser> signIn)
        {
            _manager = manager;
            _signIn = signIn;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Register(RegisterVM register)
        {
            AppUser user = new AppUser
            {
                Firstname = register.Firstname,
                LastName = register.LastName,
                UserName = register.Username,
                Email = register.Email
            };

            IdentityResult result = await _manager.CreateAsync(user, register.Password);

            if(register.Term == true)
            {
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Term", "Incredible registration system without our condidtions!");
                return View();
            }
            
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _manager.FindByNameAsync(login.Username);

            Microsoft.AspNetCore.Identity.SignInResult result = await _signIn.PasswordSignInAsync(user, login.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Incorrect password or username");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signIn.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);

            EditUserVM editUser = new EditUserVM
            {
                Firstname = user.Firstname,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email
            };

            return View(editUser);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(EditUserVM user)
        {
            AppUser existed = await _manager.FindByNameAsync(User.Identity.Name);

            EditUserVM editUser = new EditUserVM
            {
                Firstname = user.Firstname,
                LastName = user.LastName,
                Username = user.LastName
            };

            if (!ModelState.IsValid) return View(editUser);

            bool result = editUser.ConfirmPassword == null && editUser.Password == null && editUser.CurrentPassword != null;

            if(user.Email == null || user.Email != existed.Email)
            {
                ModelState.AddModelError("", "Email cannot changed");
                return View(editUser);
            }
            if (result)
            {
                existed.Firstname = user.Firstname;
                existed.LastName = user.LastName;
                existed.UserName = user.Username;
                await _manager.UpdateAsync(existed);
            }
            else
            {
                existed.Firstname = user.Firstname;
                existed.LastName = user.LastName;
                existed.UserName = user.Username;

                IdentityResult identityResult = await _manager.ChangePasswordAsync(existed, user.CurrentPassword, user.Password);

                if (!identityResult.Succeeded)
                {
                    foreach(IdentityError error in identityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View();
                }
            }

            return RedirectToAction("Index", "Home");
        }
    } 
}
