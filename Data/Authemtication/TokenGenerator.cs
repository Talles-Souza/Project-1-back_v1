using Domain.Authentication;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Data.Authemtication
{
    public class TokenGenerator : ITokenGenerator
    {
        public dynamic Generator(User user)
        {
            var claims = new List<Claim> {
            new Claim("Email", user.Email),
            new Claim("Name", user.Name),
            new Claim("Phone", user.Phone),
            new Claim("Id", user.Id.ToString())
        };

            var expires = DateTime.Now.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vgçjdmsfklgnldsanfgknsdlnglksadngnasldngklasdn"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            expires: expires,
            claims: claims
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new
            {
                acess_token = token,
                expirations = expires
            };
        }

        public dynamic GeneratorWithGoogle(User user)
        {
            var claims = new List<Claim> {
            new Claim("Email", user.Email),
            new Claim("Name", user.Name),
            new Claim("Picture", user.Picture),
            new Claim("Sub", user.Sub),
            new Claim("Service", user.Service),
            new Claim("Id", user.Id.ToString())
        };

            var expires = DateTime.Now.AddDays(1);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vgçjdmsfklgnldsanfgknsdlnglksadngnasldngklasdn"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
            expires: expires,
            claims: claims
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new
            {
                acess_token = token,
                expirations = expires
            };
        }
    }
}
