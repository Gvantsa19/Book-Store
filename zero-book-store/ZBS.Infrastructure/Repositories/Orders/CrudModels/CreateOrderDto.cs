using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ZBS.Infrastructure.Models.Enum;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ZBS.Infrastructure.Repositories.Orders.CrudModels
{
    public class CreateOrderDto
    {
        public int UserId { get; set; } 
        public string Address { get; set; } 
        public string Phone { get; set; } 
        public PaymentType paymentType { get; set; }  
        public Status status { get; set; }
    }
}
