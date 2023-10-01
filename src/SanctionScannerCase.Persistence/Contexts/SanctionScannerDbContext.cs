using Microsoft.EntityFrameworkCore;
using SanctionScannerCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Contexts
{
    public class SanctionScannerDbContext : DbContext
    {
        public SanctionScannerDbContext(DbContextOptions<SanctionScannerDbContext> opt):base(opt)
        {

        }
        //Assemblyleri aldım ve burada entity konfigürasyonlarını set ettim.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #region DBset

        public DbSet<Book> Books { get; set; }

        #endregion DBset
    }
}
