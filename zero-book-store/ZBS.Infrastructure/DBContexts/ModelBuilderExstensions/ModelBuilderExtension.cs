using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models;
using ZBS.Shared.Helpers;

namespace ZBS.Infrastructure.DBContexts.ModelBuilderExstensions
{
    public static class ModelBuilderExtension
    {
        private static PasswordHelper passwordHelper = new PasswordHelper();

        public static void SeedAdmin(this ModelBuilder modelBuilder)
        {
            var (password, Salt) = passwordHelper.CreateHash("Admin123");

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Admin1",
                    LastName = "Admin1",
                    Email ="o_chaganava@yahoo.com",
                    Password = Convert.ToBase64String(password),
                    Salt= Convert.ToBase64String(Salt),
                    Address = "Tbilisi",
                    Role = Infrastructure.Models.Enum.Role.Admin,
                    DateCreated = DateTime.Now
                });
        }

        public static void SeedBookCategory(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookCategory>().HasData(
                new BookCategory { Id=1, Name= "Action", DateCreated= DateTime.Now },
                new BookCategory { Id=2, Name= "Classics",DateCreated=DateTime.Now },
                new BookCategory { Id = 3, Name = "Fantasy", DateCreated = DateTime.Now });
        }
    }
}
