using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiravRu.Models;
using KiravRu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersApiController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;

        public UsersApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("getUsers")]
        public IActionResult GetUsers(string search)
        {
            var result = _userManager.Users.ToList();
            return Ok(new { users = result });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("create")]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok(model);
                }
                else
                {
                    List<string> errors = new List<string>();
                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }
                    return Ok(new { errors = errors });
                }
            }
            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("getUser")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == "0")
            {
                return Ok(new { user = new EditUserViewModel() });
            }
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Ok(null);
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, UserName = user.UserName };
            return Ok(new { user = model });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("edit")]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return Ok(model);
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            List<string> errors = new List<string>();
                            foreach (var errorText in result.Errors)
                            {
                                errors.Add(errorText.Description);
                            }
                            return Ok(new { errors = errors });
                        }
                    }
                }
            }
            return Ok(model);
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("delete")]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Ok(null);
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return Ok(new { user = model });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<ApplicationUser>)) as IPasswordValidator<ApplicationUser>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<ApplicationUser>)) as IPasswordHasher<ApplicationUser>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await _userManager.UpdateAsync(user);
                        var responce = new EditUserViewModel { Id = user.Id, Email = user.Email };
                        return Ok(new { user = responce });
                    }
                    else
                    {
                        List<string> errors = new List<string>();
                        foreach (var errorText in result.Errors)
                        {
                            errors.Add(errorText.Description);
                        }
                        return Ok(new { errors = errors });
                    }
                }
                else
                {
                    List<string> errors = new List<string>();
                    errors.Add("The User is not found");
                    return Ok(new { errors = errors });
                }
            }
            return Ok(model);
        }
    }
}