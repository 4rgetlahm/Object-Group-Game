using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Exceptions
{
    public class SessionUpdateFailException : Exception
    {
        public SessionUpdateFailException(string message) : base(message)
        {

        }
    }
}
