using System;
using NLog;

namespace RC.ADS.WebAPP.Comm
{
    public static class RCLog
    {
        private static ILogger logger = NLog.LogManager.GetCurrentClassLogger();


        public static void Debug(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Debug(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
                logger.Debug(string.Format("{0}: {1}", "Null", msg));
            }
            
        }

        public static void Info(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Info(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
                logger.Info(string.Format("{0}: {1}", "Null", msg));
            }
           
        }

        public static void Warn(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Warn(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
                logger.Warn(string.Format("{0}: {1}","Null", msg));
            }
        }

        public static void Error(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Error(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
                logger.Error(string.Format("{0}: {1}", "Null", msg));
            }
            
        }

        public static void Fatal(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Fatal(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
             
                logger.Fatal(string.Format("{0}: {1}", "Null", msg));
            }
          
        }

        public static void Trace(object sender, string msg)
        {
            if (sender != null)
            {
                Type type = sender.GetType();
                logger.Trace(string.Format("{0}: {1}", type.FullName, msg));
            }
            else
            {
                logger.Trace(string.Format("{0}: {1}", "Null", msg));
            }
            
        }
    }
}
 