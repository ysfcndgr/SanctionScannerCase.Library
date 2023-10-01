using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Common.Results
{
    //Kendi custom result sınıfımı oluşturdum
    public class OperationResult<T>
    {
        public bool IsSuccessfull { get; }
        public string Message { get; }
        public T Data { get; }
        public OperationResult(bool issuccessfull, string message = null, T data = default)
        {
            IsSuccessfull = issuccessfull;
            Message = message;
            Data = data;
        }
    }
}
