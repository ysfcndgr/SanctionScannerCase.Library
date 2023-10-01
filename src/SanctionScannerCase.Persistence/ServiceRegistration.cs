using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Persistence.Contexts;
using SanctionScannerCase.Persistence.Repositories.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence
{
    public static class ServiceRegistration
    {
        //Service registrationları yaptım ve sqlserver connection stringi appsettings.json dosyasından okudum b öylelikle yol değiştiğinde appsettings.json dosyasındaki içeriği değiştirdiğimizde deploy almadan değişiklik gerçekleşmiş olacaktır.
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SanctionScannerDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SanctionScannerDb"));
            });
            services.AddScoped<IBookWriteRepository, BookWriteRepository>();
            services.AddScoped<IBookReadRepository, BookReadRepository>();
            services.AddScoped<ILoggingService,SerilogLoggingService>();
        }
    }
}
