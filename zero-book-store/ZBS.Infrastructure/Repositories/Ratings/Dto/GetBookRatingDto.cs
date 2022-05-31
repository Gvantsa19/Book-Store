using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Ratings.Dto
{
    public class GetBookRatingDto
    { 
        public float Rating { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime DateofPublish { get; set; }
        public string Publisher { get; set; }
        public int NumberOfPages { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

    }
}
