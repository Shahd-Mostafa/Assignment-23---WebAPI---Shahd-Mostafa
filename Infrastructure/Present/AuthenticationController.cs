using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Present
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserResultDto>> Login([FromBody] LoginDto loginDto)
        {
            var result = await _serviceManager.authenticationServices.LoginAsync(loginDto);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserResultDto>> Register([FromBody] RegisterDto registerDto)
        {
            return Ok(await _serviceManager.authenticationServices.RegisterAsync(registerDto));
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserResultDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.authenticationServices.GetcurrentUserAsync(email);
            return Ok(result);
        }
        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.authenticationServices.GetUserAddress(email);
            return Ok(result);
        }
        [HttpPut("UpdateAddress")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(CreateAddressDto addressDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.authenticationServices.UpdateUserAddress(email!, addressDto);
            return Ok(result);
        }
    }
}
