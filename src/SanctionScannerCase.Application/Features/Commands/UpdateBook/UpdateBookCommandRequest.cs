using MediatR;
using Microsoft.AspNetCore.Http;
using SanctionScannerCase.Application.Features.Commands.CreateBook;
using SanctionScannerCase.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Commands.UpdateBook
{
    public class UpdateBookCommandRequest : IRequest<UpdateBookCommanResponse>
    {
        public string Id { get; set; }
        public string Borrower { get; set; }
        public string Date { get; set; }
    }
}
