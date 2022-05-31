using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Books.CRUDmodels
{
    public class CreateBookModel
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateofPublish { get; set; }
        public string Publisher { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
    }
}
