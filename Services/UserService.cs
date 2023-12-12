using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BE_WiseWallet.Services
{
    public class UserService : IUserService
    {
        public readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task<ApplicationUser> GetUserById(string Id)
        {
           return _context.Users
                          .Include(u => u.Teams)
                            .ThenInclude(t => t.Members)
                          .Include(u => u.Image)
                          .SingleOrDefaultAsync(u => u.Id == Id);
        }
    }
}
