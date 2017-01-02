﻿using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Core;
using Serilog.Enrichers;


namespace DistributedLogging.Console
{
    class Program
    {


        static void Main(string[] args)
        {
            MyLogger log = new MyLogger();
            log.DoLog();

        }
    }

    class MyLogger
    {
        LoggerConfiguration logconf;
        ILogger logger;

        public MyLogger()
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


            logger = logconf.CreateLogger().ForContext(typeof(MyLogger));



        }
        public void DoLog()
        {

            int rand;

            try
            {
                for (int I = 0; I < 100; I++)
                {

                    Random r = new Random();
                    rand = r.Next();

                    if (rand % 2 == 0)
                        logger.Information("This is an information log submitted at {time}", DateTime.Now);
                    else
                        logger.Error("This is an error submitted at {time}", DateTime.Now);

                }

                throw new Exception("I have to fail");

            }
            catch (Exception e) when (LogError(e))
            {

            }


        }

        bool LogError(Exception ex)
        {
            logger.Error(ex, "Unhandled Exception with Snapshot");
            return true;
        }

    }

}