using MediatR;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetBook
{
    public class GetBookQueryHandler : IRequestHandler<GetBookQueryRequest, GetBookQueryResponse>
    {
        private readonly IBookReadRepository _bookReadRepository;
        private readonly ILoggingService _loggingService;
        public GetBookQueryHandler(IBookReadRepository bookReadRepository,ILoggingService loggingService)
        {
            _bookReadRepository = bookReadRepository;
            _loggingService = loggingService;
        }
        public async Task<GetBookQueryResponse> Handle(GetBookQueryRequest request, CancellationToken cancellationToken)
        {
            var model = await _bookReadRepository.GetByIdAsync(request.Id);
            if (!model.IsSuccessfull)
            {
                _loggingService.LogError(model.Message);
                return new()
                {
                    IsSuccessfull = false,
                    ErrorMessage = model.Message
                };
            }
            return new()
            {
                ErrorMessage = model.Message,
                IsSuccessfull = true,
                Borrower = model.Data?.BorrowerName,
                LoanDate = model.Data?.LoanDate.HasValue == true ? model.Data?.LoanDate : DateTime.Now,
                Id = model.Data?.Id.ToString()
            };
        }
    }
}
