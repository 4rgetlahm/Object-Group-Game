using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Exceptions
{
    public class BadResponseException : Exception
    {
        public BadResponseException(string message) : base(message)
        {

        }
    }
}
