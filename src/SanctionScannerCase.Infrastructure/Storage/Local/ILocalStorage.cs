using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Infrastructure.Storage.Local
{
    public interface ILocalStorage
    {
        Task<string> SaveFileAsync(IFormFile file);
    }
}
