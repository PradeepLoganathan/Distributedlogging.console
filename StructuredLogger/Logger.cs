using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuredLogger
{
    public class Logger
    {
        LoggerConfiguration logconf;
        ILogger logger;

        public Logger()
        {

            var levelSwitch = new LoggingLevelSwitch();
            levelSwitch.MinimumLevel = Serilog.Events.LogEventLevel.Verbose;

            logconf = new LoggerConfiguration()
                      .MinimumLevel.ControlledBy(levelSwitch)
                      .Enrich.WithMachineName()
                      .Enrich.WithProcessId()
                      .Enrich.WithThreadId()
                      .Enrich.WithProperty("Environment", "Production")
                      .Enrich.WithProperty("Module", "Configuration")
                      .Enrich.With(new StateIDEnricher())
                      .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                      {
                          AutoRegisterTemplate = true,
                          MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                          TemplateName = "Serilog-events-template",
                          IndexFormat = "test-{0:yyyy.MM}"
                      });


            logger = logconf.CreateLogger().ForContext(typeof(Logger));
        }
  
    }
}
