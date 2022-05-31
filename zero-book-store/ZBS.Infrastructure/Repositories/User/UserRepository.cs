using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Models.Enum;
using ZBS.Infrastructure.Repositories.User.Dto;
using ZBS.Shared.Helpers;

namespace ZBS.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {

        private DbcontextDapper _dbcontextDapper;

        public UserRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }
        public async Task<UserEntity> DeleteUserById(int Id)
        {
            using var con = _dbcontextDapper.OpenConnection();

            var user = await con.QueryFirstOrDefaultAsync<UserEntity>(@"SELECT * FROM [dbo].[User] where [User].[Id] = @Id", new { Id = Id });


            ////var user = await con.GetAsync<UserEntity>(Id);


            user.DateDeleted = DateTime.Now;

            await con.UpdateAsync(user);

            return user;
        }

        public async Task<IEnumerable<UserEntity>> GetAllUsersAsync(int currentPageNumber, int pageSize)
        {
            using var con= _dbcontextDapper.OpenConnection();
            int maxPagSize = 50;
            pageSize = (pageSize > 0 && pageSize <= maxPagSize) ? pageSize : maxPagSize;

            int skip = (currentPageNumber - 1) * pageSize;
            int take = pageSize;

            string query = @"SELECT  COUNT(*) FROM [User]
                             SELECT  * FROM [User] ORDER BY Id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            var reader = con.QueryMultiple(query, new { Skip = skip, Take = take });

            int count = reader.Read<int>().FirstOrDefault();

            List<UserEntity> allTodos = reader.Read<UserEntity>().ToList();

            var result = new PagingResponseModel<List<UserEntity>>(allTodos, count, currentPageNumber, pageSize);
            return result.Data;
                //return await con.QueryAsync<UserEntity>(@"SELECT *
                //            FROM [zero-book-store].[dbo].[User]
  
                //            SELECT  * FROM Todo
                //            ORDER BY Id
                //            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY");
        }

        public async Task<UserEntity> GetUserByIdAsync(int Id)
        {
            using var con = _dbcontextDapper.OpenConnection();
            return await con.GetAsync<UserEntity>(Id);
        }

        public async Task<GetUserProfileDto> UpdateUserProfile(UpdateUserProfileDto dto)
        {
            using var conn = _dbcontextDapper.OpenConnection();

            return await conn.QueryFirstOrDefaultAsync<GetUserProfileDto>(@" begin update [User]
                      set  FirstName = @FirstName, LastName =@LastName, Email = @Email,  MobileNumber=@MobileNumber, DateOfBirth = @DateOfBirth, Education = @Education,  Address = @Address
                        where Id = @Id
                        begin select  FirstName, LastName, Email, MobileNumber, DateOfBirth, Education , Address from [User]  where Id = @Id
                        end
                    end
                 ", dto);
        }

        public async Task<UserEntity> UpdateUserRole(int Id, Role role)
        {
            using var con = _dbcontextDapper.OpenConnection();

            var user = await con.QueryFirstOrDefaultAsync<UserEntity>(@"SELECT * FROM [dbo].[User] where [User].[Id] = @Id", new { Id = Id });

            user.Role = role;
            user.DateUpdated = DateTime.Now;

            await con.UpdateAsync(user);

            return user;
        }
    }
}
