using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Repositories
{
    //Klasik generic repository kullanmak yerine write ve read işlemlerini ayırdım.Başka bir veritabanından veri okuyup başka bir veritabanına yazma işlemi yapılabilir.
    public interface IReadRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        OperationResult<IQueryable<TEntity>> GetAll(bool tracking = true);
        Task<OperationResult<TEntity>> GetByIdAsync(string id, bool tracking = true);
        Task<OperationResult<TEntity>> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = true);
        OperationResult<IQueryable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true);
    }
}
