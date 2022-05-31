using System.ComponentModel.DataAnnotations;

namespace ZBS.API.Models.Rating
{
    public class AddRatingModel
    {
        [Range(1, 5)]
        public int Rating { get; set; } 
        public int Id { get; set; }
    }
}
