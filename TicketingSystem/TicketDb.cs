using System;
using System.Linq;

namespace TicketingSystem
{
    public class TicketDb
    {
        public BugDb bugs { get; set; }
        public TaskDb tasks { get; set; }
        public EnhancementDb enhancements { get; set; }

        public TicketDb()
        {
            bugs = new BugDb();
            tasks = new TaskDb();
            enhancements = new EnhancementDb();
        }

        public void AccessDb()
        {
            bool access = true;
            while (access)
            {
                MainMenu();
                switch (Validate.ValidateMenuSelection(Console.ReadLine(), 8))
                {
                    case "1":
                        Console.WriteLine("\n===Bugs:");
                        bugs.Print();
                        Console.WriteLine("\n===Tasks:");
                        tasks.Print();
                        Console.WriteLine("\n===Enhancements:");
                        enhancements.Print();
                        break;
                    case "2":
                        Console.WriteLine("\n===Bugs:");
                        bugs.Print();
                        break;
                    case "3":
                        Console.WriteLine("\n===Tasks:");
                        tasks.Print();
                        break;
                    case "4":
                        Console.WriteLine("\n===Enhancements:");
                        enhancements.Print();
                        break;
                    case "5":
                        Console.WriteLine("\n===Add Ticket Type:");
                        TypeMenu();
                        switch (Validate.ValidateMenuSelection(Console.ReadLine(), 4))
                        {
                            case "1":
                                bugs.Add();
                                break;
                            case "2":
                                tasks.Add();
                                break;
                            case "3":
                                enhancements.Add();
                                break;
                        }
                        break;
                    case "6":
                        Console.WriteLine("\n===Remove Ticket Type:");
                        TypeMenu();
                        switch (Validate.ValidateMenuSelection(Console.ReadLine(), 4))
                        {
                            case "1":
                                bugs.Remove();
                                break;
                            case "2":
                                tasks.Remove();
                                break;
                            case "3":
                                enhancements.Remove();
                                break;
                        }
                        break;
                    case "7":
                        SearchMenu();
                        break;
                    case "8":
                        Console.WriteLine("===Exiting Database");
                        access = false;
                        break;
                }
            }
        }

        public void MainMenu()
        {
            Console.Write("\n===MainMenu===\n" +
                              "=1) Print All Tickets\n" +
                              "=2) Print Bug Tickets\n" +
                              "=3) Print Task Tickets\n" +
                              "=4) Print Enhancement Tickets\n" +
                              "=5) Add Ticket\n" +
                              "=6) Remove Ticket\n" +
                              "=7) Search Tickets\n" +
                              "=8) Exit Program\n" +
                              "===");
        }

        public void TypeMenu()
        {
            Console.Write("\n===Ticket Types===\n" +
                          "=1)Bug/Defect Ticket\n" +
                          "=2)Task Ticket\n" +
                          "=3)Enhancement Ticket\n" +
                          "=4)Return to Main Menu\n" +
                          "===");
        }

        public void SearchMenu()
        {
            Console.Write("\n===Search By===\n" +
                          "=1)Status\n" +
                          "=2)Priority\n" +
                          "=3)Submitter\n" +
                          "=4)Return to Main Menu\n" +
                          "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 4))
            {
                case "1":
                    SearchStatus();
                    break;
                case "2":
                    SearchPriority();
                    break;
                case "3":
                    SearchSubmitter();
                    break;
            }
        }

        public void SearchStatus()
        {
            Console.Write("\n===Search By Status For ______ Tickets\n" +
                "=1)Open\n" +
                "=2)Closed\n" +
                "=3)Return to Main Menu\n" +
                "===");
            switch(Validate.ValidateMenuSelection(Console.ReadLine(), 3))
            {
                case "1":
                    var openBugs = bugs.Bugs.Where(b => b.Status == Status.Open);
                    var openTasks = tasks.Tasks.Where(t => t.Status == Status.Open); ;
                    var openEnhancmenets = enhancements.Enhancements.Where(t => t.Status == Status.Open);
                    Console.Write($"===Tickets found: {(openBugs.Count() + openTasks.Count() + openEnhancmenets.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach(Bug b in openBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach(Task t in openTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach(Enhancement e in openEnhancmenets)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
                case "2":
                    var closedBugs = bugs.Bugs.Where(b => b.Status == Status.Closed);
                    var closedTasks = tasks.Tasks.Where(t => t.Status == Status.Closed); ;
                    var closedEnhancmenets = enhancements.Enhancements.Where(t => t.Status == Status.Closed);
                    Console.Write($"===Tickets found: {(closedBugs.Count() + closedTasks.Count() + closedEnhancmenets.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach (Bug b in closedBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach (Task t in closedTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach (Enhancement e in closedEnhancmenets)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
            }
        }

        public void SearchPriority()
        {
            Console.Write("\n===Search By Priority For ______ Priority Tickets\n" +
                "=1)Negligible\n" +
                "=2)Low\n" +
                "=3)Medium\n" +
                "=4)High\n" +
                "=5)Return to Main Menu\n" +
                "===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 5))
            {
                case "1":
                    var negligbleBugs = bugs.Bugs.Where(b => b.Priority == Priority.Negligible);
                    var negligibleTasks = tasks.Tasks.Where(t => t.Priority == Priority.Negligible);
                    var negligibleEnhancements = enhancements.Enhancements.Where(e => e.Priority == Priority.Negligible);
                    Console.Write($"===Tickets found: {(negligbleBugs.Count() + negligibleTasks.Count() + negligibleEnhancements.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach (Bug b in negligbleBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach (Task t in negligibleTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach (Enhancement e in negligibleEnhancements)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
                case "2":
                    var lowBugs = bugs.Bugs.Where(b => b.Priority == Priority.Low);
                    var lowTasks = tasks.Tasks.Where(t => t.Priority == Priority.Low);
                    var lowEnhancements = enhancements.Enhancements.Where(e => e.Priority == Priority.Low);
                    Console.Write($"===Tickets found: {(lowBugs.Count() + lowTasks.Count() + lowEnhancements.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach (Bug b in lowBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach (Task t in lowTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach (Enhancement e in lowEnhancements)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
                case "3":
                    var mediumBugs = bugs.Bugs.Where(b => b.Priority == Priority.Medium);
                    var mediumTasks = tasks.Tasks.Where(t => t.Priority == Priority.Medium);
                    var mediumEnhancements = enhancements.Enhancements.Where(e => e.Priority == Priority.Medium);
                    Console.Write($"===Tickets found: {(mediumBugs.Count() + mediumTasks.Count() + mediumEnhancements.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach (Bug b in mediumBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach (Task t in mediumTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach (Enhancement e in mediumEnhancements)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
                case "4":
                    var highBugs = bugs.Bugs.Where(b => b.Priority == Priority.High);
                    var highTasks = tasks.Tasks.Where(t => t.Priority == Priority.High);
                    var highEnhancements = enhancements.Enhancements.Where(e => e.Priority == Priority.High);
                    Console.Write($"===Tickets found: {(highBugs.Count() + highTasks.Count() + highEnhancements.Count())}\n" +
                        $"===Display Resulting Tickets?\n" +
                        $"=1)Yes\n" +
                        $"=2)No\n" +
                        $"===");
                    switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
                    {
                        case "1":
                            foreach (Bug b in highBugs)
                            {
                                b.Display();
                                Console.WriteLine();
                            }
                            foreach (Task t in highTasks)
                            {
                                t.Display();
                                Console.WriteLine();
                            }
                            foreach (Enhancement e in highEnhancements)
                            {
                                e.Display();
                                Console.WriteLine();
                            }
                            break;
                    }
                    break;
            }
        }

        public void SearchSubmitter()
        {
            Console.Write("\n===Search by Name of Submitter===\n" +
                "===");
            string searchName = Validate.ValidateName(Console.ReadLine());
            var namedBugs = bugs.Bugs.Where(b => b.Submitter.ToLower().Equals(searchName.ToLower()));
            var namedTasks = tasks.Tasks.Where(t => t.Submitter.ToLower().Equals(searchName.ToLower()));
            var namedEnhancements = enhancements.Enhancements.Where(e => e.Submitter.ToLower().Equals(searchName.ToLower()));
            Console.Write($"===Tickets found: {(namedBugs.Count() + namedTasks.Count() + namedEnhancements.Count())}\n" +
                $"===Display Resulting Tickets?\n" +
                $"=1)Yes\n" +
                $"=2)No\n" +
                $"===");
            switch (Validate.ValidateMenuSelection(Console.ReadLine(), 2))
            {
                case "1":
                    foreach (Bug b in namedBugs)
                    {
                        b.Display();
                        Console.WriteLine();
                    }
                    foreach (Task t in namedTasks)
                    {
                        t.Display();
                        Console.WriteLine();
                    }
                    foreach (Enhancement e in namedEnhancements)
                    {
                        e.Display();
                        Console.WriteLine();
                    }
                    break;
            }
        }
        
    }
}