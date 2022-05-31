using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ZBS.Infrastructure.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        [Display(Name = "Customer")]
        Customer = 1,
        [Display(Name = "Admin")]
        Admin = 2,
        [Display(Name = "Delivery")]
        Delivery = 3
    }
}
