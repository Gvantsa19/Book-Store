using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class EmailTakenException : AppException
    {
        public EmailTakenException()
        {
        }
        public EmailTakenException(string message) : base(message)
        {
        }
        public EmailTakenException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
