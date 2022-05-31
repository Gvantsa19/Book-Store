using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Cart.Dto
{
    public class DeleteItemDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
