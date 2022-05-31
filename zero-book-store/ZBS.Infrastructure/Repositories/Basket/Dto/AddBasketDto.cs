using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  
using DataAnnotationsExtensions;

namespace ZBS.Infrastructure.Repositories.Cart.Dto
{
    public class AddBasketDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        [Min(1)]
        public int Quantity { get; set; }
         
    }
}
