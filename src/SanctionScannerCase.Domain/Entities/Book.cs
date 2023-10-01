using SanctionScannerCase.Common.Enums;
using SanctionScannerCase.Domain.Base;

namespace SanctionScannerCase.Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string Photo { get; set; }
        //State boolean olabilirdi ancak enum olmasının daha doğru olacağını düşündüm.
        public BookState State { get; set; }
        public string? BorrowerName { get; set; }
        public DateTime? LoanDate { get; set; }
    }
}
