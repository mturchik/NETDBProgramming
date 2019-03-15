using System;
using System.Net.NetworkInformation;

namespace TicketingSystem
{
    public enum Status
    {
        Closed, Open
    }

    static class StatusMethods
    {
        public static Status GetStatus(string s)
        {
            switch (s)
            {
                case "Closed":
                    return Status.Closed;
                case "Open":
                    return Status.Open;
                default:
                    return Status.Closed;
            }
        }

        public static string GetString(Status s)
        {
            switch (s)
            {
                case Status.Closed:
                    return "Closed";
                case Status.Open:
                    return "Open";
                default:
                    return "Closed";
            }
        }
    }
}