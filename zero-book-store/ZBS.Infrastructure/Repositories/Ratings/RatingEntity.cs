using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Ratings
{
    public class RatingEntity : BaseEntity
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int Rating { get; set; }
    }
}
