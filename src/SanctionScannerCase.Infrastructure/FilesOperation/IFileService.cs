using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Infrastructure.FilesOperation
{
    public interface IFileService
    {
        public bool IsImageFile(IFormFile file);
    }
}
