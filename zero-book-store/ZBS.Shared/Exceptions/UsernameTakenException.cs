using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class UsernameTakenException : AppException
    {
        public UsernameTakenException()
        {
        }
        public UsernameTakenException(string message) : base(message)
        {
        }
        public UsernameTakenException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
