using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SanctionScannerCase.Application.Repositories;
using SanctionScannerCase.Common.Logging;
using SanctionScannerCase.Common.Results;
using SanctionScannerCase.Domain.Base;
using SanctionScannerCase.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Repositories
{
    public class WriteRepository<TEntity> : IWriteRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SanctionScannerDbContext _context;
        private readonly ILoggingService _loggingService;
        public WriteRepository(SanctionScannerDbContext context,ILoggingService loggingService)
        {
            _context = context;
            _loggingService = loggingService;
        }
        public DbSet<TEntity> Table => _context.Set<TEntity>();
        //Yazma kaydetme işlemlerini yaptım.OperationResult'ı doldurdum. Save methodunu ayırdım böylelikle unitofwork design patterninide uyguladım
        public async Task<OperationResult<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> entry = await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return new OperationResult<TEntity>(true, "İşlem başarılı", entry.Entity);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Kaydetme işleminde hata oluştu",ex);
                return new OperationResult<TEntity>(false, "İşlem başarısız: " + ex.Message);
            }
        }
        public async Task<OperationResult<bool>> AddRangeAsync(List<TEntity> entities)
        {
            try
            {
                await Table.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Toplu kaydetme işleminde hata oluştu", ex);

                return new OperationResult<bool>(false, "Ekleme işlemi başarısız: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> RemoveAsync(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> entry = Table.Remove(entity);
                await _context.SaveChangesAsync();
                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Silme işleminde hata oluştu", ex);
                return new OperationResult<bool>(false, "Silme işlemi başarısız: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> RemoveAsync(string id)
        {
            try
            {
                TEntity model = await _context.Set<TEntity>().FindAsync(Guid.Parse(id));
                if (model == null)
                {
                    _loggingService.LogError("Nesne bulunamadı");
                    return new OperationResult<bool>(false, "Nesne bulunamadı");
                }

                EntityEntry<TEntity> entry = _context.Set<TEntity>().Remove(model);
                await _context.SaveChangesAsync();

                return new OperationResult<bool>(true, "İşlem başarılı", true);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("İşlem başarısız",ex);
                return new OperationResult<bool>(false, "İşlem başarısız: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> RemoveRangeAsync(List<TEntity> entities)
        {
            try
            {
                Table.RemoveRange(entities);
                await _context.SaveChangesAsync();
                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Silme işlemi başarısız", ex);
                return new OperationResult<bool>(false, "Silme işlemi başarısız: " + ex.Message);
            }
        }

        public async Task<OperationResult<int>> SaveAsync()
        {
            try
            {
                int savedChanges = await _context.SaveChangesAsync();
                return new OperationResult<int>(true, data: savedChanges);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Veri tabanına kaydetme işlemi başarısız", ex);
                return new OperationResult<int>(false, "Veritabanına kaydetme işlemi başarısız: " + ex.Message);
            }
        }

        public async Task<OperationResult<bool>> UpdateAsync(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> entry = Table.Update(entity);
                await _context.SaveChangesAsync();
                return new OperationResult<bool>(true);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Güncelleme işlemi başarısız", ex);
                return new OperationResult<bool>(false, "Güncelleme işlemi başarısız: " + ex.Message);
            }
        }

    }
}
