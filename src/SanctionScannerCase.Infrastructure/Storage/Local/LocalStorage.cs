using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace SanctionScannerCase.Infrastructure.Storage.Local
{
    public class LocalStorage: ILocalStorage
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        //wwwroot altına dosyayı kaydetme işlemini uyguladım.
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

    }
}
