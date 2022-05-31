using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class NotValidCardException : Exception
    {
        public NotValidCardException()
        {
        }

        public NotValidCardException(string message) : base(message)
        {
        }

        public NotValidCardException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
