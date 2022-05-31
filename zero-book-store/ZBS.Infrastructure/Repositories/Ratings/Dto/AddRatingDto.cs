using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Repositories.Ratings.Dto
{
    public class AddRatingDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
       
    }
}
