using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using SanctionScannerCase.Application.Features.Commands.CreateBook;
using SanctionScannerCase.Application.Repositories.Book;
using SanctionScannerCase.Common.Enums;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Domain.Entities;
using SanctionScannerCase.Infrastructure.FilesOperation;
using SanctionScannerCase.Infrastructure.Storage.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.UnitTest
{
    public class CreateBookCommmandHandlerTests
    {
        [Test]
        //Örnek olması açısından 2 adet unit test yazdım
        // IBookWriteRepository,localstorage ve fileservice interfacelerinin mock bir uygulamasını oluşturdum
        public async Task Create_Book_With_Here_State_Should_Return_Success()
        {
            // Arrange
            var bookWriteRepositoryMock = new Mock<IBookWriteRepository>();
            var localStorageMock = new Mock<ILocalStorage>();
            var fileMock = new Mock<IFileService>();
            var loggingMock = new Mock<ILoggingService>();
            //CreateBookCommandHandler sınıfını mock datalarla oluşturuyorum
            var createBookHandler = new CreateBookCommandHandler(bookWriteRepositoryMock.Object, localStorageMock.Object, fileMock.Object, loggingMock.Object);
            
            //oluşturulan book requesti için yeni bir request oluşturuyorum
            var request = new CreateBookCommandRequest
            {
                State = BookState.Here,
                name = "Alice Harikalar Diyarında",
                author = "Lewis Carroll",
                bookphoto = null,
            };

            //aşağıdaki methodlarda başarılı bir şekilde dönmesi için mock ile ilgili davranışları ayarlıyorum
            bookWriteRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ReturnsAsync(new OperationResult<Book>(true, "Başarılı"));
            localStorageMock.Setup(storage => storage.SaveFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("test.jpg");

            fileMock.Setup(w => w.IsImageFile(It.IsAny<IFormFile>())).Returns(true);


            // Act
            //oluşturulan isteği handler sınıfına iletiyorum
            var response = await createBookHandler.Handle(request, CancellationToken.None);

            // Assert
            //ve issuccessfull durumunu kontrol ediyorum.
            Assert.True(response.IsSuccessfull);
        }

        [Test]
        public async Task Create_Book_With_Borrowed_State_Should_Return_Success()
        {
            // Arrange
            var bookWriteRepositoryMock = new Mock<IBookWriteRepository>();
            var localStorageMock = new Mock<ILocalStorage>();
            var fileMock = new Mock<IFileService>();
            var loggingMock = new Mock<ILoggingService>();

            var createBookHandler = new CreateBookCommandHandler(bookWriteRepositoryMock.Object, localStorageMock.Object, fileMock.Object,loggingMock.Object);


            var request = new CreateBookCommandRequest
            {
                State = BookState.Borrowed,
                name = "Simülakrlar ve Simülasyon",
                author = "Jean Baudrillard",
                bookphoto = null,
                borrower = "yusuf",
                loandate = "2023-10-08"
            };

            bookWriteRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Book>())).ReturnsAsync(new OperationResult<Book>(true, "Başarılı"));
            localStorageMock.Setup(storage => storage.SaveFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("test.jpg");

            fileMock.Setup(w => w.IsImageFile(It.IsAny<IFormFile>())).Returns(true); 

            // Act
            var response = await createBookHandler.Handle(request, CancellationToken.None);

            // Assert
            Assert.True(response.IsSuccessfull);
        }
    }
}
