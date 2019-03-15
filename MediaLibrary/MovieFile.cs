using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaLibrary
{
    class MovieFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public string filePath { get; set; }
        public List<Movie> Movies { get; set; }

        public MovieFile(string path)
        {
            Movies = new List<Movie>();
            filePath = path;
            //populate list with new movie objects
            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    //create new movie
                    Movie book = new Movie();
                    //get movie line
                    string line = sr.ReadLine();
                    //get movie ID by splicing out from first comma
                    book.mediaId = UInt64.Parse(line.Substring(0, line.IndexOf(',')));
                    //remove info from string
                    line = line.Remove(0, line.IndexOf(',') + 1);

                    //Get running time from end of string
                    string[] runTime = line.Substring(line.LastIndexOf(',') + 1).Split();
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));
                    //parse string info into ints before inputing into movie
                    int[] intRunTime = { 0, 0, 0 };
                    for(var i = 0; i < runTime.Length - 1; i++)
                    {
                        intRunTime[i] = int.Parse(runTime[i]);
                    }
                    book.runningTime = new TimeSpan(intRunTime[0], intRunTime[1], intRunTime[2]);

                    //get director info from string
                    book.director = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //get Genres from string
                    //break genres down along | and pass array to movie
                    string[] genres = line.Substring(line.LastIndexOf(',') + 1).Split('|');
                    foreach(string genre in genres)
                    {
                        book.genres.Add(genre);
                    }
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //add movie title to last field of movie
                    book.title = line;

                    //add movie to list
                    Movies.Add(book);
                }
                sr.Close();
                logger.Info("Movies in file {Count}", Movies.Count);
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        //check if title input is already in file
        public bool isUniqueTitle(string title)
        {
            if (Movies.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate movie title {Title}", title);
                return false;
            }
            return true;
        }

        public void DisplayMovies()
        {
            foreach(Movie m in Movies)
            {
                Console.WriteLine(m.Display());
            }
        }

        public void AddMovie()
        {
            try
            {

                //title, genres, director, running time
                Movie movie = new Movie();

                // first generate movie id
                movie.mediaId = Movies.Max(m => m.mediaId) + 1;

                //get title from user
                Console.Write("Movie Title: ");
                string title = Console.ReadLine();
                while (!isUniqueTitle(title))
                {
                    Console.Write("Duplicate movie title...\n" +
                                  "Movie Title: ");
                    title = Console.ReadLine();
                }
                // if title contains a comma, wrap it in quotes
                title = title.IndexOf(',') != -1 ? $"\"{title}\"" : title;
                movie.title = title;

                //get name of director from user
                Console.Write("Movie Director: ");
                string director = Console.ReadLine();
                while (!Validate.ValidateName(director))
                {
                    Console.Write("Movie Director: ");
                    director = Console.ReadLine();
                }
                movie.director = director;

                //get list of genres from user
                string genre;
                do
                {
                    Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                  "Movie Genre: ");
                    genre = Console.ReadLine();
                    while (!Validate.ValidateWord(genre))
                    {
                        Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                      "Movie Genre: ");
                        genre = Console.ReadLine();
                    }
                    if (!genre.ToLower().Equals("end"))
                    {
                        movie.genres.Add(genre);
                    }
                } while (!genre.ToLower().Equals("end"));

                //get running time of movie
                Console.Write("Enter running time of movie in hours, minutes, and seconds.\n" +
                              "Movie Run Time- Hours: ");
                string hours = Console.ReadLine();
                while (!Validate.ValidateNumber(hours))
                {
                    Console.Write("Enter running time of movie in hours, minutes, and seconds.\n" +
                                  "Movie Run Time- Hours: ");
                    hours = Console.ReadLine();
                }
                Console.Write("Movie Run Time- Minutes: ");
                string minutes = Console.ReadLine();
                while (!Validate.ValidateNumber(minutes))
                {
                    Console.Write("Movie Run Time- Minutes: ");
                    minutes = Console.ReadLine();
                }
                Console.Write("Movie Run Time- Seconds: ");
                string seconds = Console.ReadLine();
                while (!Validate.ValidateNumber(seconds))
                {
                    Console.Write("Movie Run Time- Seconds: ");
                    seconds = Console.ReadLine();
                }
                movie.runningTime = new TimeSpan(int.Parse(hours), int.Parse(minutes), int.Parse(seconds));

                //Write to file
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{movie.mediaId},{title},{string.Join("|", movie.genres)},{movie.director},{movie.runningTime}");
                sw.Close();

                // add movie details to Lists
                Movies.Add(movie);

                // log transaction
                logger.Info("Movie id {Id} added", movie.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void SearchMovies()
        {
            Console.WriteLine("Title)\t");
            
            string criteria = Console.ReadLine();

            var searchResults = Movies.Where(m => m.title.ToLower().Contains(criteria.ToLower()));
            Console.WriteLine($"There are {searchResults.Count()} movies that contain \"{criteria}\"\n" +
                $"Display?\n" +
                $"1) Yes\n" +
                $"2) No\n" +
                $")) ");

            string input = Console.ReadLine();
            do
            {
                switch (input)
                {
                    case "1":
                        foreach (Movie m in searchResults)
                        {
                            Console.WriteLine(m.Display());
                        }
                        Console.WriteLine($"Results: {searchResults.Count()} movies");
                        input = "2";
                        break;
                    case "2":
                        break;
                    default:
                        Console.WriteLine("Input error.\n" +
                            "Display?\n" +
                            "1) Yes\n" +
                            "2) No\n" +
                            ")) ");
                        input = Console.ReadLine();
                        break;
                }
            } while (input != "2");
        }
    }
}
