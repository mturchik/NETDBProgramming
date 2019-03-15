using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace MediaLibrary
{
    class MainClass
    {
        // create a class level instance of logger (can be used in methods other than Main)
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
            logger.Info("Program started");
            MovieFile movieFile = new MovieFile("../../movies.scrubbed.csv");
            AlbumFile albumFile = new AlbumFile("../../albums.csv");
            BookFile bookFile = new BookFile("../../books.csv");
            string menuInput;
            //loop main menu while input is within bounds for the menu
            do
            {
                Menu();
                menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        movieFile.AddMovie();
                        break;
                    case "2":
                        movieFile.DisplayMovies();
                        break;
                    case "3":
                        movieFile.SearchMovies();
                        break;
                    case "4":
                        albumFile.AddAlbum();
                        break;
                    case "5":
                        albumFile.DisplayAlbums();
                        break;
                    case "6":
                        albumFile.SearchAlbums();
                        break;
                    case "7":
                        bookFile.AddBooks();
                        break;
                    case "8":
                        bookFile.DisplayBooks();
                        break;
                    case "9":
                        bookFile.SearchBooks();
                        break;
                }
            } while (menuInput != "0");

            logger.Info("Program ended");
        }

        public static void Menu()
        {
            Console.WriteLine("1) Movie \t:Add");
            Console.WriteLine("2) Movie \t:Display");
            Console.WriteLine("3) Movie \t:Search By Title");
            Console.WriteLine("4) Album \t:Add");
            Console.WriteLine("5) Album \t:Display");
            Console.WriteLine("6) Album \t:Search By Title");
            Console.WriteLine("7) Book \t:Add");
            Console.WriteLine("8) Book \t:Display");
            Console.WriteLine("9) Book \t:Search By Title");
            Console.WriteLine("0) Quit");
                Console.Write(")) ");
        }
        
    }
}
