using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
