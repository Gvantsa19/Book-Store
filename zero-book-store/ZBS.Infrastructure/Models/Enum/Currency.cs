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
    public enum Currency
    {
        [Display(Name = "GEL")]
        GEL,
        [Display(Name = "USD")]
        USD,
        [Display(Name = "EUR")]
        EUR
    }
}
