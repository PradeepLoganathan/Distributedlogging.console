using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;


namespace DistributedLogging.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerConfiguration logconf;
            ILogger logger;
            try
            {
                logconf = new LoggerConfiguration()
                          .MinimumLevel.Is(Serilog.Events.LogEventLevel.Verbose)
                          .WriteTo.LiterateConsole()
                          .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                          {
                              AutoRegisterTemplate = true,
                              MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose,
                              TemplateName = "Serilog-events-template",
                              IndexFormat = "test-{0:yyyy.MM}"
                          });
                          

                logger = logconf.CreateLogger();

                for (int I = 0; I < 100000; I++)
                {
                    
                    logger.Error("THis is the informaton");
                }
            }

            catch (Exception e)
            {
                 
            }

            
                                         
        }
    }


    
}
