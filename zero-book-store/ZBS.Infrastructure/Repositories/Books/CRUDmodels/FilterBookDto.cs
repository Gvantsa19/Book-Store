using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Books.CRUDmodels
{
    public class FilterBookDto
    {
        public string? Title { get; set; }
        public string? CategoryName { get; set; }
    }
}
