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
        private LoggerConfiguration logconf;
        private ILogger log;

        public Logger()
        {

            
        }

        public ILogger Log
        {
            get
            {
                return this.log;
            }
        }

        public void Initialize()
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


            log = logconf.CreateLogger().ForContext(typeof(Logger));
            log.Information("THis is the intialization message");

        }
  
    }
}
