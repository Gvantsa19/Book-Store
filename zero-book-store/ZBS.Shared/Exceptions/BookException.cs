using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class BookException :Exception
    {
        public BookException()
        {
        }

        public BookException(string message) : base(message)
        {
        }

        public BookException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
