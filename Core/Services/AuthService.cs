﻿using Domain.Exceptions;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) throw new UnAuthorizedException();
            var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!flag) new UnAuthorizedException();

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Token"
            };
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description);
                throw new ValidationException(errors);
            }

            return new UserResultDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Token"
            };
        }
    }
}
