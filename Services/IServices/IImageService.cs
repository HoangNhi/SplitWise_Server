using BE_WiseWallet.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BE_WiseWallet.Services.IServices
{
    public interface IImageService
    {
        public Task<PhysicalFileResult> GetImage(string fileName);
        public Task<Image> Upload(IFormFile file);
        public Task DeleteImage(Image image);
    }
}
