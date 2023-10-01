using MediatR;
using SanctionScannerCase.Application.Repositories;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Infrastructure.FilesOperation;
using SanctionScannerCase.Infrastructure.Storage.Local;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Commands.CreateBook
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommandRequest, CreateBookCommandResponse>
    {
        private readonly IBookWriteRepository _bookWriteRepository;
        private readonly ILocalStorage _localStorage;
        private readonly IFileService _fileService;
        private readonly ILoggingService _loggingService;

        //Bağımlılıkları constructor ile dolduruyorum

        public CreateBookCommandHandler(IBookWriteRepository bookWriteRepository, ILocalStorage localStorage, IFileService fileService, ILoggingService loggingService)
        {
            _bookWriteRepository = bookWriteRepository;
            _localStorage = localStorage;
            _fileService = fileService;
            _loggingService = loggingService;

        }
        public async Task<CreateBookCommandResponse> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
        {

            #region Check Integrity

            //Front tarafında yaptığım kontrolleri aynı şekilde backendde yapıyorum burayı kalabalık tutmak yrine bir method yardımı ile bunu yapıyorum.

            var check = CheckIntegrity(request);

            if (!check.IsSuccessfull)
            {
                return new()
                {
                    IsSuccessfull = false,
                    Message = check.Message
                };
            }

            #endregion Check Integrity

            try
            {
                //Kitap durumuna göre kitabı doldurup kayıt işlemini gerçekleştiriyorum.
                if (request.State == Common.Enums.BookState.Here)
                {
                    var hereBook = new Domain.Entities.Book
                    {
                        State = Common.Enums.BookState.Here,
                        AuthorName = request.author,
                        Name = request.name,
                        Photo = await _localStorage.SaveFileAsync(request.bookphoto)
                    };

                    var operationResult = await _bookWriteRepository.AddAsync(hereBook);

                    await _bookWriteRepository.SaveAsync();

                    if (!operationResult.IsSuccessfull)
                    {
                        _loggingService.LogError("Kaydetme işleminde hata oluştu " + operationResult.Message);

                        return new CreateBookCommandResponse
                        {
                            IsSuccessfull = false,
                            Message = operationResult.Message
                        };
                    }

                    return new CreateBookCommandResponse
                    {
                        IsSuccessfull = true,
                        Message = "Kitap ekleme başarılı"
                    };
                }

                else if (request.State == Common.Enums.BookState.Borrowed)
                {
                    var loanDate = DateTime.Parse(request.loandate, CultureInfo.CurrentCulture);
                    var loanBook = new Domain.Entities.Book
                    {
                        State = Common.Enums.BookState.Borrowed,
                        AuthorName = request.author,
                        Name = request.name,
                        Photo = await _localStorage.SaveFileAsync(request.bookphoto),
                        BorrowerName = request.borrower,
                        LoanDate = loanDate
                    };

                    var result = await _bookWriteRepository.AddAsync(loanBook);

                    await _bookWriteRepository.SaveAsync();

                    if (!result.IsSuccessfull)
                    {
                        _loggingService.LogError("Kaydetme işleminde hata oluştu " + result.Message);
                        return new CreateBookCommandResponse
                        {
                            IsSuccessfull = false,
                            Message = result.Message
                        };
                    }

                    return new CreateBookCommandResponse
                    {
                        IsSuccessfull = true,
                        Message = "Kitap ekleme başarılı"
                    };
                }
                else
                {
                    _loggingService.LogWarning("Geçersiz bir kitap durumu geldi ");

                    return new CreateBookCommandResponse
                    {
                        IsSuccessfull = false,
                        Message = "Geçersiz kitap durumu"
                    };
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Kaydetme işleminde hata oluştu ", ex);
                return new CreateBookCommandResponse
                {
                    IsSuccessfull = false,
                    Message = "Bir hata oluştu: " + ex.Message
                };
            }
        }

        //Kitabın dışarıda ve kütaphanede olma durumunun ortak yanları var ayrı ayrı aynı kodu yazmaktansa ortak olanları bir kümeye toplayıp tek bir methodda kontrol ediyorum.
        private CreateBookCommandResponse CommonClusterCheck(CreateBookCommandRequest request)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                _loggingService.LogError("Kitap adı null veya boş geldi ");

                return new CreateBookCommandResponse()
                {
                    IsSuccessfull = false,
                    Message = "Kitap adı gereklidir"
                };
            }

            else if (request.author.Length < 3)
            {
                _loggingService.LogError("Yazar adı 3 karakterden az geldi.");

                return new CreateBookCommandResponse()
                {
                    IsSuccessfull = false,
                    Message = "Yazar adı 3 karakterden fazla olmalıdır"
                };
            }

            var result = _fileService.IsImageFile(request.bookphoto);
            if (!result)
            {
                _loggingService.LogError("Geçersiz bir dosya türü saptandı");

                return new CreateBookCommandResponse()
                {
                    IsSuccessfull = false,
                    Message = "Geçersiz dosya türü"
                };
            }

            return new CreateBookCommandResponse()
            {
                IsSuccessfull = true
            };
        }

        private CreateBookCommandResponse CheckIntegrity(CreateBookCommandRequest request)
        {
            if (request.State == Common.Enums.BookState.Here)
            {
                var clusterCheck = CommonClusterCheck(request);
                if (!clusterCheck.IsSuccessfull)
                {

                    return new CreateBookCommandResponse
                    {
                        IsSuccessfull = false,
                        Message = clusterCheck.Message
                    };
                }
            }

            else if (request.State == Common.Enums.BookState.Borrowed)
            {
                var clusterCheck = CommonClusterCheck(request);
                if (!clusterCheck.IsSuccessfull)
                {

                    return new CreateBookCommandResponse
                    {
                        IsSuccessfull = false,
                        Message = clusterCheck.Message
                    };
                }

                //CommonClusterCheck'in üzerine borrowed durumunda borrower ve tarih bulunuyor bunları kontrol ediyorum.
                if (string.IsNullOrEmpty(request.borrower))
                {
                    _loggingService.LogError("Ödünç alan null veya boş geldi");

                    return new CreateBookCommandResponse()
                    {
                        IsSuccessfull = false,
                        Message = "Ödünç veriliyorsa ödünç alanın adı gerekli."
                    };
                }

                var dateCheck = DateTime.TryParse(request.loandate, out DateTime formattedDate);
                if (!dateCheck)
                {
                    _loggingService.LogError("Geçersiz bir tarih türü geldi");
                    return new CreateBookCommandResponse()
                    {
                        IsSuccessfull = false,
                        Message = "Ödünç veriliyorsa geri getirme tarihi gerekli."
                    };
                }
                if (formattedDate < DateTime.Now)
                {
                    _loggingService.LogError("Ödünç verme tarihi bugünden önce geldi");
                    return new()
                    {
                        IsSuccessfull = false,
                        Message = "Ödünç verme tarihi bugünden sonra olabilir"
                    };

                }
            }

            return new CreateBookCommandResponse()
            {
                IsSuccessfull = true
            };

        }



    }
}
