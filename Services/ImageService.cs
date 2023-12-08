using BE_WiseWallet.Data;
using BE_WiseWallet.Entities;
using BE_WiseWallet.Entities.Requests;
using BE_WiseWallet.Services.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BE_WiseWallet.Services
{
    public class ImageService : IImageService
    {
        public readonly ApplicationDbContext _context;
        public readonly IWebHostEnvironment _IWebHostEnvironment;

        public ImageService(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _IWebHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteImage(Image image)
        {
            if (image == null)
            {
                return;
            }
            var rootFolder = Path.Combine(_IWebHostEnvironment.WebRootPath, "Images");
            var filePath = Path.Combine(rootFolder, image.FileName);
            var removed = _context.Images.Remove(image);
            if (removed == null)
            {
                return;
            }
            System.IO.File.Delete(filePath);
            await _context.SaveChangesAsync();
        }

        public Task<PhysicalFileResult> GetImage(string fileName)
        {
            var rootFolder = Path.Combine(_IWebHostEnvironment.WebRootPath, "Images");
            var filePath = Path.Combine(rootFolder, fileName);
            return Task.FromResult(new PhysicalFileResult(filePath, "image/jpeg"));
        }

        public Task<Image> Upload(IFormFile file)
        {
            if (file.Length <= 0)
            {
                return Task.FromResult(new Image());
            }
            var rootFolder = Path.Combine(_IWebHostEnvironment.WebRootPath, "Images");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(rootFolder, fileName);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            Image image = new Image
            {
                FileName = fileName,
                FilePath = "https://localhost:7200/static/images/" + fileName,
                FileSize = file.Length,
                OriginalFileName = file.FileName
            };
            return Task.FromResult(image);
        }
    }
}
