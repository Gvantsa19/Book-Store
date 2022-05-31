using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Application.Services.UserServ.Dtoes
{
    public class ChangePasswordDto
    {
        public int? Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ChangePasswordDto()
        {
        }

        public ChangePasswordDto(int? id, string password, string confirmPassword)
        {
            Id = id;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
    }
}
