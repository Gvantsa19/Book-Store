using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ZBS.Infrastructure.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        [Display(Name = "active")]
        active = 1,
        [Display(Name = "pending")]
        pending = 2,
        [Display(Name = "completed")]
        completed = 3
    }
}
