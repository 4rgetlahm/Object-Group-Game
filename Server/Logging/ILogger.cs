using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Logging
{
    public interface ILogger
    {
        public void Log<T>(T logData);
    }
}
