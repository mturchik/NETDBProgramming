using System;

namespace TicketingSystem
{
    public class Enhancement : Ticket
    {
        public string Software { get; set; }
        public double Cost { get; set; }
        public string Reason { get; set; }
        public string Estimate { get; set; }

        public Enhancement() { }

        public Enhancement(string[] enhancementInfo)
        {
            this.TicketId = int.Parse(enhancementInfo[0]);
            this.Summary = enhancementInfo[1];
            this.Status = StatusMethods.GetStatus(enhancementInfo[2]);
            this.Priority = PriorityMethods.GetPriority(enhancementInfo[3]);
            this.Submitter = enhancementInfo[4];
            this.Assigned = enhancementInfo[5];
            this.Watching = enhancementInfo[6];
            this.Software = enhancementInfo[7];
            this.Cost = double.Parse(enhancementInfo[8]);
            this.Reason = enhancementInfo[9];
            this.Estimate = enhancementInfo[10];
        }

        public override void Display()
        {
            Console.WriteLine("===Enhancement Ticket===\n" +
                              "=TicketID: {0}\n" +
                              "=Summary: {1}\n" +
                              "=Status: {2}\n" +
                              "=Priority: {3}\n" +
                              "=Submitter: {4}\n" +
                              "=Assigned: {5}\n" +
                              "=Watching: {6}\n" +
                              "=Software: {7}\n" +
                              "=Cost: ${8}\n" +
                              "=Reason: {9}\n" +
                              "=Estimate: {10}",
                TicketId.ToString(), Summary, Status, Priority, Submitter, Assigned, Watching, Software, Cost, Reason, Estimate);
        }

        public override string ToString() => TicketId.ToString() + "," + Summary + "," + Status + "," + Priority + "," + Submitter + "," + Assigned + "," + Watching + "," + Software + "," + Cost + "," + Reason + "," + Estimate;

    }
}