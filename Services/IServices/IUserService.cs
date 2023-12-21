using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Respones;

namespace BE_WiseWallet.Services.IServices
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetUserById(string Id);
        public Task<UserResponse> GetUserByAccessToken(string token);
    }
}
