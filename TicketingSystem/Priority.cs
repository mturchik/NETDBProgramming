namespace TicketingSystem
{
    public enum Priority
    {
        Negligible, Low, Medium, High
    }

    static class PriorityMethods
    {
        public static Priority GetPriority(string s)
        {
            switch (s)
            {
                case "Low":
                    return Priority.Low;
                case "Medium":
                    return Priority.Medium;
                case "High":
                    return Priority.High;
                default:
                    return Priority.Negligible;
            }
        }

        public static string GetString(Priority p)
        {
            switch (p)
            {
                case Priority.Low:
                    return "Low";
                case Priority.Medium:
                    return "Medium";
                case Priority.High:
                    return "High";
                default:
                    return "Negligible";
            }
        }
    }
}