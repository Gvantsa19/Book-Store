using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;

namespace ZBS.Application.Services.BookServ.Dtoes
{
    public class CreateBookWithAuthorDto
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateofPublish { get; set; }
        public string Publisher { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public Language Language { get; set; }
    }
}
