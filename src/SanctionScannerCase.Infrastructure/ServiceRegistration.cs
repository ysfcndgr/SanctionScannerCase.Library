using Microsoft.Extensions.DependencyInjection;
using SanctionScannerCase.Infrastructure.FilesOperation;
using SanctionScannerCase.Infrastructure.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Infrastructure
{
    public static class ServiceRegistration
    {
        //Service registrationları yaptım.
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ILocalStorage, LocalStorage>();
            services.AddSingleton<IFileService, FileService>();
        }
    }
}
