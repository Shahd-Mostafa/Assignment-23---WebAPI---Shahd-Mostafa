using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<User> _userManager ) : IAuthenticationService
    {
        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var validEmail = new EmailAddressAttribute().IsValid(loginDto.Email);
            var user = validEmail ? await _userManager.FindByEmailAsync(loginDto.Email) : await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
                throw new UnAuthorizedException();
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword)
                throw new UnAuthorizedException();
            return new UserResultDto
            (
                user.Id,
                user.DisplayName ?? " ",
                user.Email ?? " ",
                "Token"
            );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingEmail= await _userManager.Users.AnyAsync(u=> u.Email == registerDto.Email);
            var existingUserName = await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName);
            if (existingEmail)
                throw new BadRequestException("Email already exists");
            if (existingUserName)
                throw new BadRequestException("UserName already exists");

            var user = new User
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber
            };
            var addUserResult= await _userManager.CreateAsync(user, registerDto.Password);
            if (!addUserResult.Succeeded)
            {
                throw new BadRequestException(string.Join("\n",addUserResult.Errors.Select(e=>e.Description)));
            }
            return new UserResultDto
            (
                user.Id,
                user.DisplayName ?? " ",
                user.Email ?? " ",
                "Token"
            );
        }
    }
}
