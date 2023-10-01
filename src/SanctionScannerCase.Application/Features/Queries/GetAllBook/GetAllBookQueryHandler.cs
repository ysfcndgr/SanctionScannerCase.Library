using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetAllBook
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQueryRequest, GetAllBookQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly ILoggingService _loggingService;
        public GetAllBookQueryHandler(IBookReadRepository bookReadRepository, ILoggingService loggingService)
        {
            _bookReadRepository = bookReadRepository;
            _loggingService = loggingService;
        }
        public async Task<GetAllBookQueryResponse> Handle(GetAllBookQueryRequest request, CancellationToken cancellationToken)
        {
            #region Get Total Count

            var totalCountResult = _bookReadRepository.GetAll(false);
            if (!totalCountResult.IsSuccessfull)
            {
                _loggingService.LogError(totalCountResult.Message);
                return new GetAllBookQueryResponse()
                {
                    ErrorMessage = totalCountResult.Message
                };
            }

            var totalCount = await totalCountResult.Data.CountAsync();

            #endregion Get Total Count

            #region Get Books

            var bookResult = _bookReadRepository.GetAll(false);
            if (!bookResult.IsSuccessfull)
            {
                _loggingService.LogError(bookResult.Message);

                return new GetAllBookQueryResponse()
                {
                    ErrorMessage = bookResult.Message
                };
            }

            var books = await bookResult.Data.Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .OrderBy(x => x.Name)
                .Select(p => new
                {
                    p.Id,
                    p.AuthorName,
                    p.Photo,
                    p.BorrowerName,
                    p.Name,
                    p.LoanDate,
                    p.State
                }).ToListAsync();

            #endregion Get Books

            return new GetAllBookQueryResponse()
            {
                TotalBookCount = totalCount,
                Books = books
            };

        }
    }
}
