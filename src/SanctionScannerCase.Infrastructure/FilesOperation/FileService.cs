using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Infrastructure.FilesOperation
{
    public class FileService:IFileService
    {
        //Aşağıdaki methoda IFormFile verildiğinde .jpeg jpg ve png olma durumuna göre true veya false döndürdüm. Yine aynı şekilde file boş gelirse veya uzunluğu 0 gelirse yine false döndürdüm
        public bool IsImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }

            var allowedExtensions = new[] { ".jpeg", ".jpg", ".png" };

            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return Array.IndexOf(allowedExtensions, fileExtension) != -1;
        }
    }
}
