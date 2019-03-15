using System;
using System.Collections.Generic;
using System.IO;

namespace TicketingSystem
{
    public class TaskDb : IAccessible
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public List<Task> Tasks { get; set; } = new List<Task>();
        private const string File = "../../Tasks.csv";

        public TaskDb()
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
                    //project name
                    tickInfo.Add(line.Substring(0, line.IndexOf(',')));
                    line = line.Remove(0, line.IndexOf(',') + 1);
                    //due date
                    tickInfo.Add(line.Substring(0));

                    Tasks.Add(new Task(tickInfo.ToArray()));
                }
                sr.Close();
            }
        }

        public void Add()
        {
            //create placeholder ticket
            Task fresh = new Task();

            //set ticket #
            if (Tasks.Count == 0)
            {
                fresh.TicketId = 1;
            }
            else
            {
                fresh.TicketId = Tasks[Tasks.Count - 1].TicketId + 1;
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

            //get project name
            Console.Write("=Enter Name of the Project: ");
            fresh.ProjectName = Validate.ValidateForChars(Console.ReadLine());

            //get due date
            Console.Write("=Enter Project Due Date: ");
            fresh.DueDate = Validate.ValidateDate(Console.ReadLine());
            
            //confirm ticket
            Console.WriteLine("=Ticket information to be entered:");
            fresh.Display();
            Console.Write("=Confirm?\n" +
                          "=1) Add\n" +
                          "=2) Cancel\n" +
                          "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
            {
                case "1":
                    logger.Trace("Ticket added, {fresh}", fresh);
                    Console.WriteLine("Ticket Added.");
                    Tasks.Add(fresh);
                    StreamWriter sw = new StreamWriter(File);
                    foreach (var task in Tasks)
                    {
                        sw.WriteLine(task.ToString());
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
            foreach (var task in Tasks)
            {
                if (task.TicketId == int.Parse(input))
                {
                    index = Tasks.IndexOf(task);
                }
            }

            if (index == -1)
            {
                Console.WriteLine("=No Matching TicketID, returning to main menu.");
            }
            else
            {
                Console.WriteLine("=Ticket Found.");
                Tasks[index].Display();
                Console.Write("=Confirm?\n" +
                              "=1) Delete\n" +
                              "=2) Cancel\n" +
                              "===");
                switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                {
                    case "1":
                        logger.Trace("Ticket Deleted, {ticket}", Tasks[index]);
                        Console.WriteLine("=Ticket Deleted.");
                        Tasks.RemoveAt(index);
                        StreamWriter sw = new StreamWriter(File);
                        foreach (var task in Tasks)
                        {
                            sw.WriteLine(task.ToString());
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
            foreach (var task in Tasks)
            {
                task.Display();
                Console.WriteLine();
            }
        }
    }
}