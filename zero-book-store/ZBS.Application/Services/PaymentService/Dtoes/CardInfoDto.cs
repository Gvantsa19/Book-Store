using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Application.Services.PaymentServ.Dtoes
{
    public class CardInfoDto
    {
        [RegularExpression(@"^[0-9]+$"),MaxLength(19)]
        public string CardNumber { get; set; }
        [Range(100,999)]
        public int CVC { get; set; }
        [Range(1,12)]
        public int Month { get; set; }
        [Range(1,99)]
        public int Year { get; set; }
    }
}
