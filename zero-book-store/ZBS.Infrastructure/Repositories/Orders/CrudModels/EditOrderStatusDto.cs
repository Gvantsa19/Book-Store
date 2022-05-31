using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZBS.Infrastructure.Models.Enum;
using System.Text.Json.Serialization; 
using Newtonsoft.Json.Converters;

namespace ZBS.Infrastructure.Repositories.Orders.CrudModels
{
    public class EditOrderStatusDto
    {
        public int UserId { get; set; }
        public int Id { get; set; } 
        public Status status { get; set; }
    }
}
