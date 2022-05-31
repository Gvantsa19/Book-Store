using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZBS.Shared.Exceptions
{
    public class AuthorException : Exception
    {
        public AuthorException()
        {

        }
        public AuthorException(string message) : base(message)
        {
        }

        public AuthorException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
