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
    public class RatingsConfiguration : IEntityTypeConfiguration<Ratings>
    {
        public void Configure(EntityTypeBuilder<Ratings> builder)
        {
            builder.HasOne(x => x.user)
              .WithMany()
              .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.book)
                .WithMany()
                .HasForeignKey(x => x.Id);
        }
    }
}
