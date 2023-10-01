using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SanctionScannerCase.Application.Features.Queries.GetAllBook;
using System.Threading.Tasks;

namespace SanctionScanner.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Örnek olması açısından sadece mediator kullanarak rahat bir şekilde api'sini yazdım. Controllerın temiz olmasına özen gösterdim.
        [HttpGet("GetBooks")]
        public async Task<ActionResult<GetAllBookQueryResponse>> GetBooks([FromQuery] int page, [FromQuery] int size)
        {
            var getAllBookQueryRequest = new GetAllBookQueryRequest
            {
                Page = page,
                Size = size
            };

            var model = await _mediator.Send(getAllBookQueryRequest);

            return Ok(model);
        }
    }
}
