using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetAllBook
{
    public class GetAllBookQueryRequest:IRequest<GetAllBookQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}
