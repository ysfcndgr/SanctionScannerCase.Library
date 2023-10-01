using Microsoft.EntityFrameworkCore;
using SanctionScannerCase.Application.Repositories;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Domain.Base;
using SanctionScannerCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Repositories
{
    public class ReadRepository<TEntity> : IReadRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SanctionScannerDbContext _context;
        private readonly ILoggingService _loggingService;
        public ReadRepository(SanctionScannerDbContext context,ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }

        public DbSet<TEntity> Table => _context.Set<TEntity>();
        //Okuma listeleme işlemlerini yaptım. Aşağıdaki fonksiyonlarda trackingi devre dışı bırakma opsiyonunu koydum listeleme ve diğer işlemlerde bir değişiklik yapılmayacağı için nesnelerinin boşuna track edilmemesini sağlayarak optimizasyon uygulamış oldum. Kendi hata sınıfımı kullanarak operationresult'ı doldurdum
        public OperationResult<IQueryable<TEntity>> GetAll(bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }
                return new OperationResult<IQueryable<TEntity>>(true, data: query);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Listeleme işlemi başarısız", ex);
                return new OperationResult<IQueryable<TEntity>>(false, ex.Message);
            }
        }

        public async Task<OperationResult<TEntity>> GetByIdAsync(string id, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }

                TEntity entity = await Table.FindAsync(Guid.Parse(id));
                if (entity != null)
                {
                    return new OperationResult<TEntity>(true, data: entity);
                }
                else
                {
                    _loggingService.LogError("GetByIdAsync Nesne bulunamadı");
                    return new OperationResult<TEntity>(false, "GetByIdAsync Nesne bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("GetByIdAsync", ex);
                return new OperationResult<TEntity>(false,  ex.Message);
            }
        }

        public async Task<OperationResult<TEntity>> GetSingleAsync(Expression<Func<TEntity, bool>> method, bool tracking = true)
        {
            try
            {
                var query = Table.AsQueryable();
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }
                TEntity entity = await Table.FirstOrDefaultAsync(method);
                if (entity != null)
                {
                    return new OperationResult<TEntity>(true, data: entity);
                }
                else
                {
                    _loggingService.LogError("GetSingleAsync nesne bulunamadı");
                    return new OperationResult<TEntity>(false, "GetSingleAsync  Nesne bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                _loggingService.LogError("GetSingleAsync",ex);
                return new OperationResult<TEntity>(false, ex.Message);
            }
        }

        public OperationResult<IQueryable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> method, bool tracking = true)
        {
            try
            {
                var query = Table.Where(method);
                if (!tracking)
                {
                    query = query.AsNoTracking();
                }
                return new OperationResult<IQueryable<TEntity>>(true, data: query);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("GetWhere", ex);
                return new OperationResult<IQueryable<TEntity>>(false,  ex.Message);
            }
        }

    }
}
