using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class SalesException : Exception
    {
        public SalesException()
        {

        }
        public SalesException(string message) : base(message)
        {
        }

        public SalesException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
