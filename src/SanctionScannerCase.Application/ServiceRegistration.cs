using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SanctionScannerCase.Application.Features.Commands.CreateBook;
using SanctionScannerCase.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application
{
    public static class ServiceRegistration
    {
        //Service registrationlarını yaptım
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ServiceRegistration));
            
            services.AddScoped<ILoggingService, SerilogLoggingService>();
        }
    }
}
