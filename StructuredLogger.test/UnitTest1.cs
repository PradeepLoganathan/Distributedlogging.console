using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructuredLogger;

namespace StructuredLogger.test
{
    [TestClass]
    public class UnitTest1
    {
        StructuredLogger.Logger logger;

        [TestMethod]
        public void TestMethod1()
        {
            int rand;
            logger = new Logger();
            logger.Initialize();

            try
            {
                for (int I = 0; I < 100; I++)
                {
                    Random r = new Random();
                    rand = r.Next();

                    if (rand % 2 == 0)
                        logger.Log.Information("This is an information log submitted at {time}", DateTime.Now);
                    else
                        logger.Log.Error("This is an error submitted at {time}", DateTime.Now);

                }

                throw new Exception("I have to fail");

            }
            catch (Exception e) when (LogError(e))
            {

            }
        }

        bool LogError(Exception ex)
        {
            logger.Log.Error(ex, "Unhandled Exception with Snapshot");
            return true;
        }
    }
}
