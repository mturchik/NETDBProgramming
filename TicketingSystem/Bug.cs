using System;

namespace TicketingSystem
{
    public class Bug : Ticket
    {
        public Severity Severity { get; set; }

        public Bug(){}

        public Bug(string[] bugInfo)
        {
            this.TicketId = int.Parse(bugInfo[0]);
            this.Summary = bugInfo[1];
            this.Status = StatusMethods.GetStatus(bugInfo[2]);
            this.Priority = PriorityMethods.GetPriority(bugInfo[3]);
            this.Submitter = bugInfo[4];
            this.Assigned = bugInfo[5];
            this.Watching = bugInfo[6];
            this.Severity = SeverityMethods.GetSeverity(bugInfo[7]);
        }

        public override void Display()
        {
            Console.WriteLine("===Bug Ticket===\n" +
                              "=TicketID: {0}\n" +
                              "=Summary: {1}\n" +
                              "=Status: {2}\n" +
                              "=Priority: {3}\n" +
                              "=Submitter: {4}\n" +
                              "=Assigned: {5}\n" +
                              "=Watching: {6}\n" +
                              "=Severity: {7}", 
                            TicketId.ToString(),Summary,Status,Priority,Submitter,Assigned,Watching,Severity);
        }

        public override string ToString() => TicketId.ToString() + "," + Summary + "," + Status + "," + Priority + "," + Submitter + "," + Assigned + "," + Watching + "," + Severity;

    }
}