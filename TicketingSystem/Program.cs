using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace TicketingSystem
{
    class Program
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Console.WriteLine("Ticket Management System.");
            TicketDb tickets = new TicketDb();
            tickets.AccessDb();
        }
        
    }
}
