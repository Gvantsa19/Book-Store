using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Authors.CrudModels;

namespace ZBS.Application.Services.AuthorService
{
    public interface IAuthorService
    {
        Task Create(CreateAuthorModel entity);
        Task<GetAuthorModel> GetById(int id);
    }
}
