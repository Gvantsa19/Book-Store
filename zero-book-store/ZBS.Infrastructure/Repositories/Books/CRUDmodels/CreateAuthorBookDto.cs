using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Books.CRUDmodels
{
    public class CreateAuthorBookDto
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
