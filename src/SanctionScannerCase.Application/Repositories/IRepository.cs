using Microsoft.EntityFrameworkCore;
using SanctionScannerCase.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Application.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        //Write ve Read operasyonlarında ortak kullanacağım Dbseti ekledim.
        DbSet<TEntity> Table { get; }
    }
}
