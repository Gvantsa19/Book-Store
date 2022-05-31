using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class RoleException : AppException
    {
        public RoleException()
        {
        }
        public RoleException(string message) : base(message)
        {
        }
        public RoleException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
