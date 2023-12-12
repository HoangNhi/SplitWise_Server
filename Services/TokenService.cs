using BE_WiseWallet.Entities;
using BE_WiseWallet.Services.IServices;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BE_WiseWallet.Services
{
    public class TokenService : ITokenService
    {
        public List<Claim> GetClaimsFromExpiredToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            return jwtSecurityToken.Claims.ToList();
        }
    }
}
