using System;
using System.IO;
using NLog;

namespace SleepData
{
    class MainClass
    {
        public static void Main(string[] args)
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
            logger.Info("Program started");

            // ask for input
            Console.WriteLine("Enter 1 to create data file.");
            Console.WriteLine("Enter 2 to parse data.");
            Console.WriteLine("Enter anything else to quit.");
            // input response
            string resp = Console.ReadLine();

            // specify path for data file
            string file = "data.txt";

            if (resp == "1")
            {
                // create data file

                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                //int weeks = int.Parse(Console.ReadLine());
                string ans = Console.ReadLine();
                if (!int.TryParse(ans, out int weeks))
                {
                    logger.Error("Invalid input (integer): {Answer}", ans);
                }
                else
                {
                    // determine start and end date
                    DateTime today = DateTime.Now;
                    // we want full weeks sunday - saturday
                    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                    // subtract # of weeks from endDate to get startDate
                    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));

                    // random number generator
                    Random rnd = new Random();

                    // create file
                    StreamWriter sw = new StreamWriter(file);
                    // loop for the desired # of weeks
                    while (dataDate < dataEndDate)
                    {
                        // 7 days in a week
                        int[] hours = new int[7];
                        for (int i = 0; i < hours.Length; i++)
                        {
                            // generate random number of hours slept between 4-12 (inclusive)
                            hours[i] = rnd.Next(4, 13);
                        }
                        // M/d/yyyy,#|#|#|#|#|#|#
                        //Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                        // add 1 week to date
                        dataDate = dataDate.AddDays(7);
                        logger.Info("Sleep data for{Date} added to data file.", dataDate);
                    }
                    sw.Close();
                }
            }
            else if (resp == "2")
            {
                // TODO: parse data file
                if (File.Exists(file))
                {
                    //file reader
                    StreamReader sr = new StreamReader(file);
                    //while stuff left to read
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        DateTime date = DateTime.Parse(line.Split(',')[0]);
                        Console.WriteLine($"Week of {date:MMM}, {date:dd}, {date:yyyy}");
                        Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg" +
                                        "\n -- -- -- -- -- -- -- --- ---");
                        string[] slept = line.Split(',')[1].Split('|');
                        int tot = 0;
                        foreach (string s in slept)
                        {
                            tot += int.Parse(s);
                        }
                        double avg = (double)tot / 7;
                        Console.WriteLine($" {slept[0],2} {slept[1],2} {slept[2],2} {slept[3],2} {slept[4],2} {slept[5],2} {slept[6],2} {tot,3} {avg,3:n1}");
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                    logger.Warn("File does not exist. {file}", file);
                }
            }
            logger.Info("Program ended.");
        }
    }
}
