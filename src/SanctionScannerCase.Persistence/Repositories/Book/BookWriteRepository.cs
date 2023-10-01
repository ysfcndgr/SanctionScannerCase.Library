using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Repositories.Book
{
    public class BookWriteRepository : WriteRepository<SanctionScannerCase.Domain.Entities.Book>, IBookWriteRepository
    {
        public BookWriteRepository(SanctionScannerDbContext context,ILoggingService loggingService) : base(context, loggingService)
        {
        }
    }
}
