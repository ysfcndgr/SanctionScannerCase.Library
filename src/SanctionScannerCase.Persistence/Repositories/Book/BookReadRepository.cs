using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Domain.Entities;
using SanctionScannerCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Repositories.Book
{
    public class BookReadRepository : ReadRepository<SanctionScannerCase.Domain.Entities.Book>, IBookReadRepository
    {
        public BookReadRepository(SanctionScannerDbContext context,ILoggingService loggingService) : base(context,loggingService)
        {
        }
    }
}
