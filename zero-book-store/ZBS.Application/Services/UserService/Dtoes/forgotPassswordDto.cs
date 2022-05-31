using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Application.Services.UserServ.Dtoes
{
    public class forgotPassswordDto
    {
        [EmailAddress]
        [Required]
        public string email { get; set; }
    }
}
