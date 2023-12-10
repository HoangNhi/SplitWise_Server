using BE_WiseWallet.Entities;

namespace BE_WiseWallet.Services.IServices
{
    public interface IUserService
    {
        public Task<ApplicationUser> GetUserById(string Id);
    }
}
