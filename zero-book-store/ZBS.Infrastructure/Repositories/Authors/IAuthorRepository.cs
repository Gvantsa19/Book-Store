using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Authors.CrudModels;

namespace ZBS.Infrastructure.Repositories.Authors
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorEntity>> GetAllAsync();
        Task<AuthorEntity> GetByIdAsync(int id);
        Task<AuthorEntity> CreateAsync(CreateAuthorModel entity);
        Task<AuthorEntity> UpdateAsync(UpdateAuthorModel entity);
    }
}
