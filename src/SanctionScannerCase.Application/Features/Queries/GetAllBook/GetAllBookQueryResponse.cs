using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Features.Queries.GetAllBook
{
    public class GetAllBookQueryResponse
    {
        public int TotalBookCount { get; set; }
        public object Books { get; set; }
        public string ErrorMessage { get; set; }
    }
}
