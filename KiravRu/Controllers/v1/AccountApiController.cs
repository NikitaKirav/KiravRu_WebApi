﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using KiravRu.Controllers.v1.Model;
using KiravRu.Logic.Domain.Users;
using KiravRu.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace KiravRu.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/account")]
    [ApiController]
    //[EnableCors("AllowOrigin")]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountApiController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("me")]
        public IActionResult Me()
        {
            var claim = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            var name = claim?.Value;
            if(name.IsNullOrEmpty()) { return Ok(new { resultCode = 100 }); }
            return Ok(new { userName = name, resultCode = 0 });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequestModel model, CancellationToken ct)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var roles = await _userManager.GetRolesAsync(user);

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var authOption = new AuthOptions();
                var authSigningKey = authOption.GetSymmetricSecurityKey();
                var now = DateTime.UtcNow;
                var test = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME));

                var token = new JwtSecurityToken(
                    issuer: authOption.ISSUER,
                    audience: authOption.AUDIENCE,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    claims: claims,
                    signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    lifetime = AuthOptions.LIFETIME
                });
            }
            return Unauthorized(new { errorMessage = "Invalid login or password!" });

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            User user = new User { Email = model.Email, UserName = model.UserName };
            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            // добавляем пользователю роль по умолчанию USER
            var addedRoles = new List<string> { "user" };
            await _userManager.AddToRolesAsync(user, addedRoles);
            if (result.Succeeded)
            {
                // установка куки
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    return Unauthorized(new {  errorMessage = error.Description  });
                }
            }
            return Unauthorized(new { errorMessage = "Bad request" });
        }

    }
}
