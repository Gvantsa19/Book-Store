using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class PaymentException : Exception
    {
        public PaymentException()
        {
        }

        public PaymentException(string message) : base(message)
        {
        }

        public PaymentException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
