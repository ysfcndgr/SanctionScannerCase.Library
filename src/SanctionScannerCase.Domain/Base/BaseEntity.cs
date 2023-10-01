namespace SanctionScannerCase.Domain.Base
{
    public abstract class BaseEntity
    {
        //Base entity'imi oluşturup değerlerini set ettim.
        #region Base Properties
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        #endregion Base Properties
    }
}
