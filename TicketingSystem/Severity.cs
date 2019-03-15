namespace TicketingSystem
{
    public enum Severity
    {
        Miniscule, Minor, Mediocre, Major, Extreme
    }

    static class SeverityMethods
    {
        public static Severity GetSeverity(string s)
        {
            switch (s)
            {
                case "Miniscule":
                    return Severity.Miniscule;
                case "Minor":
                    return Severity.Minor;
                case "Mediocre":
                    return Severity.Mediocre;
                case "Major":
                    return Severity.Major;
                case "Extreme":
                    return Severity.Extreme;
                default:
                    return Severity.Miniscule;
            }
        }

        public static string GetString(Severity s)
        {
            switch (s)
            {
                case Severity.Minor:
                    return "Minor";
                case Severity.Mediocre:
                    return "Mediocre";
                case Severity.Major:
                    return "Major";
                case Severity.Extreme:
                    return "Extreme";
                default:
                    return "Miniscule";
            }
        }
    }
}