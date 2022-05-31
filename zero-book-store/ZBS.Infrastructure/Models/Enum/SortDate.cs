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
    public enum SortDate
    {
        [Display(Name = "asc")]
        asc,
        [Display(Name = "desc")]
        desc
    }
}
