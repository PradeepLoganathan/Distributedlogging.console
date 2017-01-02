using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DistributedLogging.Console
{
    class Program
    {
        
        public static void Main()
        {
            logger 
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
