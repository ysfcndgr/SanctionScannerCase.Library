using MediatR;
using Microsoft.AspNetCore.Http;
using SanctionScannerCase.Application.Features.Queries.GetAllBook;
using SanctionScannerCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Commands.CreateBook
{
    public class CreateBookCommandRequest : IRequest<CreateBookCommandResponse>
    {
        public string name { get; set; }
        public string author { get; set; }
        public IFormFile bookphoto { get; set; }
        public BookState State { get; set; }
        public string borrower { get; set; }
        public string loandate { get; set; }
    }
}
