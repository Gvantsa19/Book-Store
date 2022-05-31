using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Repositories.Authors;
using ZBS.Infrastructure.Repositories.Authors.CrudModels;
using ZBS.Shared.Exceptions;

namespace ZBS.Application.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorService> logger;

        public AuthorService(IAuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            this.logger = logger;
        }
        public async Task Create(CreateAuthorModel entity)
        {
            await _authorRepository.CreateAsync(entity);
        }

        public async Task<GetAuthorModel> GetById(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);

            if (author == null)
            {
                throw new AuthorException(string.Format("Sale not found"));
            }
            return new GetAuthorModel
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                DateOfBirth = author.DateOfBirth

            };
        }
    }
}
