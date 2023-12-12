using BE_WiseWallet.Entities;
using System.Security.Claims;

namespace BE_WiseWallet.Services.IServices
{
    public interface ITokenService
    {
        List<Claim> GetClaimsFromExpiredToken(string token);
    }
}
