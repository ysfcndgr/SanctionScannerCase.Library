using MediatR;
using SanctionScannerCase.Application.Features.Commands.CreateBook;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Commands.UpdateBook
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommandRequest, UpdateBookCommanResponse>
    {
        private readonly IBookWriteRepository _bookWriteRepository;
        private readonly IBookReadRepository _bookReadRepository;
        private readonly ILoggingService _loggingService;
        //Bağımlılıkları constructor yardımı ile dolduruyorum.
        public UpdateBookCommandHandler(IBookWriteRepository bookWriteRepository, IBookReadRepository bookReadRepository, ILoggingService loggingService)
        {
            _bookWriteRepository = bookWriteRepository;
            _bookReadRepository = bookReadRepository;
            _loggingService = loggingService;

        }
        public async Task<UpdateBookCommanResponse> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
        {

            #region Check Integrity

            var date = DateTime.TryParse(request.Date, out DateTime formattedDate);
            if (!date)
            {
                _loggingService.LogError("Geçersiz bir tarih türü geldi");
                return new()
                {
                    IsSuccessfull = false,
                    Message = "Geçerli bir tarih giriniz"
                };
            }
            if (string.IsNullOrEmpty(request.Borrower))
            {
                _loggingService.LogError("Ödünç alan kişi null veya boş geldi");
                return new()
                {
                    IsSuccessfull = false,
                    Message = "Ödünç alan kişi zorunludur"
                };
            }
            if (formattedDate<DateTime.Now)
            {
                _loggingService.LogError("Ödünç verme tarihi bugünden önce geldi");
                return new()
                {
                    IsSuccessfull = false,
                    Message = "Ödünç verme tarihi bugünden sonra olabilir"
                };

            }

            #endregion Check Integrity

            var model = await _bookReadRepository.GetByIdAsync(request.Id);
            if (!model.IsSuccessfull)
            {
                _loggingService.LogError(model.Message);
                return new()
                {
                    IsSuccessfull = false,
                    Message = model.Message
                };
            }
            //Update durumunda sadece tarih ve borrowername değişeceği için bunları değiştiriyorum ödünç ver durumunda borrowed olacağı için bunu elle verebilirim.
            model.Data.LoanDate = Convert.ToDateTime(request.Date);
            model.Data.BorrowerName = request.Borrower;
            model.Data.State = Common.Enums.BookState.Borrowed;
            var result = await _bookWriteRepository.UpdateAsync(model.Data);
            if (!result.IsSuccessfull)
            {
                _loggingService.LogError(result.Message);

                return new()
                {
                    IsSuccessfull = false,
                    Message = result.Message
                };
            }

            return new()
            {
                IsSuccessfull = true
            };
        }

    }
}
