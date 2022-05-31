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
    public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            builder.HasOne(b => b.book)
                .WithMany()
                .HasForeignKey(b => b.BookId);

            builder.HasOne(a => a.author)
                .WithMany()
                .HasForeignKey(a => a.AuthorId);
        }
    }
}
