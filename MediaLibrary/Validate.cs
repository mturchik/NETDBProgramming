using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NLog;

namespace MediaLibrary
{
    public static class Validate
    {
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //Returns true of false based on number of items in a list
        //Ex. Menu of 5 items, if user enters 1,2,3,4 or 5 = true, else false
        public static bool ValidateMenuSelection(string s, int n)
        {
            if (s.Length > 0)
            {
                if (int.TryParse(s.Substring(0, 1), out var sel))
                {
                    if (sel > n || sel < 1)
                    {
                        logger.Warn("Selection out of bounds.");
                        Console.WriteLine("Selection out of bounds.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    logger.Warn("Input includes non-numbers.");
                    Console.WriteLine("Input includes non-numbers.");
                    return false;
                }
            }
            else
            {
                logger.Warn("Void input.");
                Console.WriteLine("Void input.");
                return false;
            }
        }

        //Checks input for Full Name standard form(First Name [Space] Last Name)
        public static bool ValidateName(string s)
        {
            Regex rx = new Regex("^[A-Za-z]{1,15}(\\s{1})[A-Za-z]{1,15}$");
            Regex rx2 = new Regex("\\P{L}+");
            if (!rx.IsMatch(s))
            {
                logger.Warn("Name must match the following format: \"FirstName{Space}LastName\"");
                Console.WriteLine("Name must match the following format: \"FirstName{Space}LastName\"");
                return false;
            }
            else
            {
                if (rx2.IsMatch(string.Join("", s.Split(' '))))
                {
                    logger.Warn("Names cannot have special characters.");
                    Console.WriteLine("Names cannot have special characters.");
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        //Verify string input is number greater than Zero
        public static bool ValidateNumber(string s)
        {
            Regex rx = new Regex("\\D+");
            if (rx.IsMatch(s))
            {
                logger.Warn("Non-Digit detected.");
                Console.WriteLine("Non-Digit detected.");
                return false;
            }
            else
            {
                if (int.Parse(s) < 0)
                {
                    logger.Warn("Number must be positive.");
                    Console.WriteLine("Number must be positive.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        
        //Check if single word with no special characters
        public static bool ValidateWord(string s)
        {
            Regex rx = new Regex("^[A-Za-z]+$");
            if (!rx.IsMatch(s))
            {
                logger.Warn("Invalid word, english letters only.");
                Console.WriteLine("Invalid word, english letters only.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}