using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KiravRu.Models;
using KiravRu.Models.WebApi;
using KiravRu.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KiravRu.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        public RolesApiController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("getRoles")]
        public IActionResult GetRoles(string search)
        {
            var result = _roleManager.Roles.ToList();
            return Ok(new { roles = result });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [Route("createRole")]
        public async Task<IActionResult> Create(RoleApi role)
        {
            if (!string.IsNullOrEmpty(role.Name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(role.Name));
                if (result.Succeeded)
                {
                    return Ok(new { role = new { Name = role.Name } });
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
            return Ok(new { role = new { Name = role.Name } });
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("deleteRole")]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        [Route("editRole")]
        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return Ok(new { userAccess = new UserAccessEditApi(model) });
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        [Route("updateAccess")]
        public async Task<IActionResult> UpdateAccess(UpdateAccessApi request)
        {
            // получаем пользователя
            ApplicationUser user = await _userManager.FindByIdAsync(request.UserId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                //var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = request.Roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(request.Roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return Ok();
            }

            List<string> errors = new List<string>();
            errors.Add("The User is not found");
            return Ok(new { errors = errors });
        }
    }
}