using Castle.DynamicProxy;
using Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Interceptors
{
    public class ContextLogInterceptor : IInterceptor
    {
        private readonly ILogger _logger;
        
        public ContextLogInterceptor(ILogger logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {

        }
    }
}
