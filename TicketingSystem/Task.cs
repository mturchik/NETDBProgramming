using System;

namespace TicketingSystem
{
    public class Task : Ticket
    {
        public string ProjectName { get; set; }
        public DateTime DueDate { get; set; }
        
        public Task() { }

        public Task(string[] taskInfo)
        {
            this.TicketId = int.Parse(taskInfo[0]);
            this.Summary = taskInfo[1];
            this.Status = StatusMethods.GetStatus(taskInfo[2]);
            this.Priority = PriorityMethods.GetPriority(taskInfo[3]);
            this.Submitter = taskInfo[4];
            this.Assigned = taskInfo[5];
            this.Watching = taskInfo[6];
            this.ProjectName = taskInfo[7];
            this.DueDate = DateTime.Parse(taskInfo[8]);
        }

        public override void Display()
        {
            Console.WriteLine("===Task Ticket===\n" +
                              "=TicketID: {0}\n" +
                              "=Summary: {1}\n" +
                              "=Status: {2}\n" +
                              "=Priority: {3}\n" +
                              "=Submitter: {4}\n" +
                              "=Assigned: {5}\n" +
                              "=Watching: {6}\n" +
                              "=Project Name: {7}\n" +
                              "=Due Date: {8}/{9}/{10}",
                TicketId.ToString(), Summary, Status, Priority, Submitter, Assigned, Watching, ProjectName, DueDate.Month, DueDate.Day, DueDate.Year);
        }

        public override string ToString() => TicketId.ToString() + "," + Summary + "," + Status + "," + Priority + "," + Submitter + "," + Assigned + "," + Watching + "," + ProjectName + "," + DueDate;

    }
}