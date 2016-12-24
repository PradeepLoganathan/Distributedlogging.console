using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.RabbitMQ;
using Serilog.Sinks.RabbitMQ.Sinks.RabbitMQ;

namespace DistributedLogging.Console
{
    class RabbitLogger
    {
        
        public void Initialize()
        {
            var obj = new RabbitMQConfiguration();
        }

        public void Start()
        {

        }
    }
}
