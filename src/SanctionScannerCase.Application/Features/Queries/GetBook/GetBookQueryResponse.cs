using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetBook
{
    public class GetBookQueryResponse
    {
        public string? ErrorMessage { get; set; }
        public bool IsSuccessfull { get; set; }
        public string? Id { get; set; }
        public DateTime? LoanDate { get; set; }
        public string? Borrower { get; set; }
    }
}
