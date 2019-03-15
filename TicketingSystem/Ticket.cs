using System;

namespace TicketingSystem
{
    public abstract class Ticket
    {
        public int TicketId { get; set; }
        public string Summary { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string Submitter { get; set; }
        public string Assigned { get; set; }
        public string Watching { get; set; }
        
        public abstract void Display();

        public abstract override string ToString();
    }
}