using ecoshare_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ecoshare_backend.Services;

public class TokenService
{
    private IConfiguration _builderConfiguration;

    public TokenService(IConfiguration builderConfiguration)
    {
        _builderConfiguration = builderConfiguration;
    }

    public string GenerateToken(Usuario user)
    {
        Claim[] claims = new Claim[]
        {
            new Claim("username", user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("email", user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Name, user.Nome + ' ' + user.Sobrenome),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("uSx3FNPdJMC_0vE9vrlQDHMcO45J_gwSr4e4eow4I8o"));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
            (
                expires: DateTime.Now.AddMinutes(60),
                claims: claims,
                signingCredentials: signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
