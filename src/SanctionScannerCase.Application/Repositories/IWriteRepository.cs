using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Repositories
{
    //Klasik generic repository kullanmak yerine write ve read işlemlerini ayırdım.Başka bir veritabanından veri okuyup başka bir veritabanına yazma işlemi yapılabilir.
    public interface IWriteRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<OperationResult<TEntity>> AddAsync(TEntity entity);
        Task<OperationResult<bool>> AddRangeAsync(List<TEntity> entities);
        Task<OperationResult<bool>> UpdateAsync(TEntity entity);
        Task<OperationResult<bool>> RemoveAsync(TEntity entity);
        Task<OperationResult<bool>> RemoveRangeAsync(List<TEntity> entities);
        Task<OperationResult<bool>> RemoveAsync(string id);
        Task<OperationResult<int>> SaveAsync();
    }
}
