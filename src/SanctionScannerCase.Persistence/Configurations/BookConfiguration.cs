using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SanctionScannerCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanctionScannerCase.Persistence.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        //IEntityTypeConfiguration interfaceni kullanarak migration edilirken propertylerin özelliklerini set ettim.
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.Property(b => b.Name).IsRequired().HasColumnName("Name");
            builder.Property(b => b.AuthorName).IsRequired().HasColumnName("AuthorName");
            builder.Property(b => b.Photo).IsRequired().HasColumnName("Photo");
        }
    }
}
