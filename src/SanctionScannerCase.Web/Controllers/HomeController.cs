using MediatR;
using Microsoft.AspNetCore.Mvc;
using SanctionScannerCase.Application.Features.Commands.CreateBook;
using SanctionScannerCase.Application.Features.Commands.UpdateBook;
using SanctionScannerCase.Application.Features.Queries.GetAllBook;
using SanctionScannerCase.Application.Features.Queries.GetBook;
using SanctionScannerCase.Application.Repositories.Book;

namespace SanctionScannerCase.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMediator _mediator;
        //Mediator design patternini kullanarak controllerin temiz tutulmasını sağladım her bir book veya diğer nesneler için ayrı ayrı repository çağırmak yerine mediator kullandım.
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetBooks(int page, int size)
        {

            GetAllBookQueryRequest getAllBookQueryRequest = new GetAllBookQueryRequest()
            {
                Page = page,
                Size = size
            };
            var model = await _mediator.Send(getAllBookQueryRequest);

            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromForm] CreateBookCommandRequest bookCommandRequest)
        {

            var response = await _mediator.Send(bookCommandRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook([FromBody] UpdateBookCommandRequest updateBookCommandRequest)
        {

            var response = await _mediator.Send(updateBookCommandRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetBook([FromBody] GetBookQueryRequest getBookQueryRequest)
        {
            var response = await _mediator.Send(getBookQueryRequest);
            return Ok(response);
        }
    }
}
