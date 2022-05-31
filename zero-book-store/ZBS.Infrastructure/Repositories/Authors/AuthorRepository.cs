using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.DBContexts;
using ZBS.Infrastructure.Repositories.Authors.CrudModels;

namespace ZBS.Infrastructure.Repositories.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DbcontextDapper _dbcontextDapper;

        public AuthorRepository(DbcontextDapper dbcontextDapper)
        {
            _dbcontextDapper = dbcontextDapper;
        }


        public async Task<AuthorEntity> CreateAsync(CreateAuthorModel entity)
        {
            using var connection = _dbcontextDapper.OpenConnection();
            var authors = new AuthorEntity
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth,
                DateCreated = DateTime.Now
            };

            var id = await connection.InsertAsync<int, AuthorEntity>(authors);
            authors.Id = id;

            return authors;

        }

        public async Task<IEnumerable<AuthorEntity>> GetAllAsync()
        {
            using var con = _dbcontextDapper.OpenConnection();
            var cmd = @"SELECT * FROM dbo.Author";

            return await con.QueryAsync<AuthorEntity>(cmd);
        }

        public async Task<AuthorEntity> GetByIdAsync(int id)
        {
            using var con = _dbcontextDapper.OpenConnection();

            return await con.QueryFirstOrDefaultAsync<AuthorEntity>(@"
                        select * from [dbo].[Author] where Id = @Id
                        "
                        , new { Id = id });
        }

        public async Task<AuthorEntity> UpdateAsync(UpdateAuthorModel entity)
        {
            using var con = _dbcontextDapper.OpenConnection();
            var authors = await con.GetAsync<AuthorEntity>(entity.Id);

            authors.Id = entity.Id;
            authors.FirstName = entity.FirstName;
            authors.LastName = entity.LastName;
            authors.DateOfBirth = entity.DateOfBirth;
            authors.DateUpdated = DateTime.Now;
            await con.UpdateAsync(authors);

            return authors;
        }
    }
}
