using System;
using System.Linq;
using NLog;

namespace MovieLINQ
{
    class MainClass
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("../../movies.csv");
            MovieFile movieFile = new MovieFile(scrubbedFile);

            var Movies = movieFile.Movies.Where(m => m.title.Contains("(1990)"));
            Console.WriteLine($"There are {Movies.Count()} movies from 1990.");

            var validate = movieFile.Movies.Any(m => m.title.Contains("(1921)"));
            Console.WriteLine($"Any movies from 1921?\t{validate}");

            var num = movieFile.Movies.Where(m => m.title.Contains("(1921)")).Count();
            Console.WriteLine($"There are {num} movies from 1921.");

            var movies1921 = movieFile.Movies.Where(m => m.title.Contains("(1921)"));
            foreach(Movie M in movies1921)
            {
                Console.WriteLine(M.Display());
            }

            var titles = movieFile.Movies.Where(m => m.title.Contains("Shark")).Select(m => m.title);
            foreach (string t in titles)
            {
                Console.WriteLine(t);
            }

            var MoviesOrdered = movieFile.Movies.Where(m => m.title.Contains("Shark")).OrderBy(m => m.title);
            foreach (Movie m in MoviesOrdered)
            {
                Console.WriteLine(m.Display());
            }

            var FirstMovie = movieFile.Movies.First(m => m.title.StartsWith("Z", StringComparison.OrdinalIgnoreCase));
            Console.WriteLine($"First movie: {FirstMovie.title}");

            logger.Info("Program ended");
        }
    }
}
