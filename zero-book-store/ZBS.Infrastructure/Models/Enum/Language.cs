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
    public enum Language
    {
        [Display(Name = "Eng")]
        Eng = 1,
        [Display(Name = "Geo")]
        Geo = 2
    }
}
