using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models;

namespace ZBS.Infrastructure.TablesConfigurations
{
    public class BasketBookConfiguration : IEntityTypeConfiguration<BasketBook>
    {
        public void Configure(EntityTypeBuilder<BasketBook> builder)
        {
            builder.HasOne(x => x.basket)
                .WithMany()
                .HasForeignKey(x => x.BasketId);

            builder.HasOne(x => x.Book)
                .WithMany()
                .HasForeignKey(x => x.BookId);
        }
    }
}
