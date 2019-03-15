using System;
using NLog;

namespace NLogExample
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create NLog configuration
            var config = new NLog.Config.LoggingConfiguration();

            //Define logging targets
            var logFile = new NLog.Targets.FileTarget("logFile") { FileName = "log_File.txt" };
            var logConsole = new NLog.Targets.ConsoleTarget("logConsole");

            //Specify minimum log level to maximum log level and target(console, file, etc)
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logConsole);
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);

            //Apply NLog configuration
            NLog.LogManager.Configuration = config;

            //Create an instance of LogManager
            var logger = NLog.LogManager.GetCurrentClassLogger();

            //Sample log messages
            //logger.Trace("Sample Trace Message");
            //logger.Debug("Sample Debug Message");
            //logger.Info("Sample Info Message");
            //logger.Warn("Sample Warn Message");
            //logger.Error("Sample Error Message");
            //logger.Fatal("Sample Fatal Message");

            //NLog supports structured messages
            //var fruit = new[] { "bananas", "apples", "pears" };
            //logger.Info("I like to eat {fruit}", fruit);

            //Example of logging an exception
            int x = 10;
            int y = 0;
            try
            {
                Console.WriteLine(x / y);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
