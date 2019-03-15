using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Services;
using System.Xml.Schema;
using NLog;

namespace TicketingSystem
{
    public class BugDb : IAccessible
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public List<Bug> Bugs { get; set; } = new List<Bug>();
        private const string File = "../../Bugs.csv";
        
        public BugDb()
        {
            //check for file
            if (!System.IO.File.Exists(File))
            {
                logger.Error("File not found {File}", File);
            }
            else
            {
                //open file
                StreamReader sr = new StreamReader(File);
                logger.Trace("File opened, {File}", File);
                ////read file line by line
                while (!sr.EndOfStream)
                {
                    //read line in file
                    string line = sr.ReadLine();
                    //list for parts of ticket
                    List<string> tickInfo = new List<string>();
                    //ticketID
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //summary
                    tickInfo.Add(line.Substring(0, line.LastIndexOf('"') + 1));
                    line = line.Remove(0, line.LastIndexOf('"') + 2);
                    //status
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //priority
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //submitter
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //assigned
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //watchers
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //severity
                    tickInfo.Add(line.Substring(0));

                    Bugs.Add(new Bug(tickInfo.ToArray()));
                }
                sr.Close();
            }
        }

        public void Add()
        {
            //create placeholder ticket
            Bug fresh = new Bug();

            //set ticket #
            if (Bugs.Count == 0)
            {
                fresh.TicketId = 1;
            }
            else
            {
                fresh.TicketId = Bugs[Bugs.Count - 1].TicketId + 1;
            }

            //get summary
            Console.Write("=Enter Bug Summary:\n" +
                          "===");
            string input = Console.ReadLine();
            fresh.Summary = "\"" + input + "\"";

            //get status
            Console.Write("=Choose Bug Status:\n" +
                          "=1) Open\n" +
                          "=2) Closed\n" +
                          "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
            {
                case "1":
                    fresh.Status = Status.Open;
                    break;
                case "2":
                    fresh.Status = Status.Closed;
                    break;
            }

            //get priority
            Console.Write("=Choose Bug Priority:\n" +
                          "=1) High\n" +
                          "=2) Medium\n" +
                          "=3) Low\n" +
                          "=4) Negligible\n" +
                          "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 4))
            {
                case "1":
                    fresh.Priority = Priority.High;
                    break;
                case "2":
                    fresh.Priority = Priority.Medium;
                    break;
                case "3":
                    fresh.Priority = Priority.Low;
                    break;
                case "4":
                    fresh.Priority = Priority.Negligible;
                    break;
            }

            //get name of submitter
            Console.Write("=Enter Name of Submitter: ");
            fresh.Submitter = Validate.ValidateName(Console.ReadLine());

            //get name of assigned
            Console.Write("=Enter Name of Assigned: ");
            fresh.Assigned = Validate.ValidateName(Console.ReadLine());

            //get number of watchers
            Console.Write("=Enter Number of Bug Watchers: ");
            var num = Validate.ValidateNumber(Console.ReadLine());
            if (num == "0")
            {
                fresh.Watching = "No Watchers";
            }
            else
            {
                //list for watchers
                List<string> watchers = new List<string>();
                for (var i = 0; i < int.Parse(num); i++)
                {
                    //get name of watchers
                    Console.Write("=Enter the Name of the Watcher: ");
                    watchers.Add(Validate.ValidateName(Console.ReadLine()));
                }
                //join watchers with '|' and add to fresh
                fresh.Watching = String.Join("|", watchers.ToArray());
            }

            //get severity
            Console.Write("=Choose Severity of Bug:\n" +
                          "=1) Miniscule\n" +
                          "=2) Minor\n" +
                          "=3) Mediocre\n" +
                          "=4) Major\n" +
                          "=5) Extreme\n" +
                          "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 5))
            {
                case "1":
                    fresh.Severity = Severity.Miniscule;
                    break;
                case "2":
                    fresh.Severity = Severity.Minor;
                    break;
                case "3":
                    fresh.Severity = Severity.Mediocre;
                    break;
                case "4":
                    fresh.Severity = Severity.Major;
                    break;
                case "5":
                    fresh.Severity = Severity.Extreme;
                    break;
            }

            //confirm ticket
            Console.WriteLine("=Ticket information to be entered:");
            fresh.Display();
            Console.Write("=Confirm?\n=1)Add\t=2)Cancel\t=");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
            {
                case "1":
                    logger.Trace("Ticket added, {fresh}", fresh);
                    Console.WriteLine("Ticket Added.");
                    Bugs.Add(fresh);
                    StreamWriter sw = new StreamWriter(File);
                    foreach (var bug in Bugs)
                    {
                        sw.WriteLine(bug.ToString());
                    }
                    sw.Close();
                    break;
                case "2":
                    Console.WriteLine("Canceling ticket creation, returning to main menu.");
                    break;
            }
        }

        public void Remove()
        {
            Print();
            Console.Write("=Enter the TicketID of the ticket you wish to delete: ");
            string input = Validate.ValidateNumber(Console.ReadLine());

            int index = -1;
            foreach (var bug in Bugs)
            {
                if (bug.TicketId == int.Parse(input))
                {
                    index = Bugs.IndexOf(bug);
                }
            }

            if (index == -1)
            {
                Console.WriteLine("=No Matching TicketID, returning to main menu.");
            }
            else
            {
                Console.WriteLine("=Ticket Found.");
                Bugs[index].Display();
                Console.Write("=Confirm?\n" +
                              "=1) Delete\n" +
                              "=2) Cancel\n" +
                              "===");
                switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                {
                    case "1":
                        logger.Trace("Ticket Deleted, {ticket}", Bugs[index]);
                        Console.WriteLine("=Ticket Deleted.");
                        Bugs.RemoveAt(index);
                        StreamWriter sw = new StreamWriter(File);
                        foreach (var bug in Bugs)
                        {
                            sw.WriteLine(bug.ToString());
                        }
                        sw.Close();
                        break;
                    case "2":
                        Console.WriteLine("Canceling ticket deletion, returning to main menu.");
                        break;
                }
            }
        }

        public void Print()
        {
            foreach (var bug in Bugs)
            {
                bug.Display();
                Console.WriteLine();
            }
        }
    }
}