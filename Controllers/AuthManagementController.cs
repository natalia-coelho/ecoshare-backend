using ecoshare_backend.Configuration;
using ecoshare_backend.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecoshare_backend.Controllers
{
    [Route("ecoshare/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly ILogger<AuthManagementController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(
            ILogger<AuthManagementController> logger,
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _logger = logger;
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                //check if email exists
                var emailExist = await _userManager.FindByEmailAsync(requestDto.Email);

                if (emailExist != null)
                    return BadRequest("Email already exist.");

                var newUser = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Name
                };

                var isCreated = await _userManager.CreateAsync(newUser, requestDto.Password);

                if (isCreated.Succeeded)
                {
                    //Generate token
                    return Ok(new RegistrationRequestResponse()
                    {
                        Result = true,
                        Token = GenerateJwtToken(newUser)
                    });
                }
                return BadRequest(isCreated.Errors.Select(e => e.Description).ToList());
            }
            return BadRequest("Invalid request payload.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(requestDto.Email);

                if (existingUser == null)
                    return BadRequest("Invalid Authentication.");

                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, requestDto.Password);

                if (isPasswordValid)
                {
                    var generatedToken = GenerateJwtToken(existingUser);

                    return Ok(new LoginRequestResponse()
                    {
                        Token = generatedToken,
                        Result = true
                    });
                }
                return BadRequest("Invalid authentication.");

            }
            return BadRequest("Invalid request payload.");
        }
        [HttpPost]
        [Route("GetToken")]
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            return jwtToken;
        }

        [HttpPost]
        [Route("ResetPassword")] //Will be Forgot password
        public IActionResult ResetPassword([FromBody] UserPasswordResetDto requestDto)
        {
            Console.WriteLine(requestDto.Email);
            return Ok();

            // TODO: Rename this endpoint to forgot password and create a new one to actually reset it
            // 1. Check if email is in database
            // 2. Generate a password reset token
            // 3. Generate a password reset link like resetpassword?token=xxx,email=yyy
            // 4. (do last) send the link via email
            // 5. Return ok
        }

        // https://chatgpt.com/share/25a14338-ddf6-4202-aa4f-89c2b78f1fc5
        // Reset Password
        // If implemented separately can be reused in a context other than forgetting the password
        // 1. check if user exists
        // 2. forward email and token to a function in the user manager that actually resets the password if the token is valid
    }
}
