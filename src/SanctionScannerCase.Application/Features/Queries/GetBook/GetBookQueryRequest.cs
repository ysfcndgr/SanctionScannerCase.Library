using MediatR;
using SanctionScannerCase.Application.Features.Queries.GetAllBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetBook
{
    public class GetBookQueryRequest : IRequest<GetBookQueryResponse>
    {
        public string Id { get; set; }
    }
}
