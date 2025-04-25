using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<User> _userManager, IOptions<JwtOptions> options) : IAuthenticationService
    {
        private readonly JwtOptions _jwtOptions = options.Value;
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
                await CreateTokenAsync(user)
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
                await CreateTokenAsync(user)
            );
        }
        private async Task<string> CreateTokenAsync(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim("userId",user.Id),
                new Claim(ClaimTypes.Name,user.UserName ?? " "),
                new Claim(ClaimTypes.Email,user.Email?? " ")
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddDays(_jwtOptions.DurationInDays),
                claims: authClaims,
                notBefore: DateTime.Now,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
