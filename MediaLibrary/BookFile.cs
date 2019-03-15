using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaLibrary
{
    class BookFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }
        public List<Book> Books { get; set; }

        public BookFile(string path)
        {
            Books = new List<Book>();
            filePath = path;
            //populate list with new movie objects
            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    //create new book
                    Book book = new Book();
                    //get book line
                    string line = sr.ReadLine();

                    // {book.title},{string.Join(" | ", book.genres)},{book.author}

                    //get book ID by splicing out from first comma
                    book.mediaId = UInt64.Parse(line.Substring(0, line.IndexOf(',')));
                    //remove info from string
                    line = line.Remove(0, line.IndexOf(',') + 1);

                    //Get page count from end of string
                    string pageCount = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));
                    //parse string info into int before inputting into book
                    book.pageCount = UInt16.Parse(pageCount);

                    //get director info from string
                    book.publisher = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //get author info from string
                    book.author = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //get Genres from string
                    //break genres down along | and pass array to book
                    string[] genres = line.Substring(line.LastIndexOf(',') + 1).Split('|');
                    foreach (string genre in genres)
                    {
                        book.genres.Add(genre);
                    }
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //add book title to last field of book
                    book.title = line;

                    //add book to list
                    Books.Add(book);
                }
                sr.Close();
                logger.Info("Movies in file {Count}", Books.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        //check if title input is already in file
        public bool isUniqueTitle(string title)
        {
            if (Books.ConvertAll(m => m.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate book title {Title}", title);
                return false;
            }
            return true;
        }

        public void DisplayBooks()
        {
            foreach (Book b in Books)
            {
                Console.WriteLine(b.Display());
            }
        }

        public void AddBooks()
        {
            try
            {

                //title, genres, author, publisher, page count
                Book book = new Book();

                // first generate book id
                book.mediaId = Books.Max(m => m.mediaId) + 1;

                //get title from user
                Console.Write("Book Title: ");
                string input = Console.ReadLine();
                while (!isUniqueTitle(input))
                {
                    Console.Write("Duplicate book title...\n" +
                                  "Book Title: ");
                    input = Console.ReadLine();
                }
                // if title contains a comma, wrap it in quotes
                input = input.IndexOf(',') != -1 ? $"\"{input}\"" : input;
                book.title = input;

                //get author name from user
                Console.Write("Book Author: ");
                input = Console.ReadLine();
                while (!Validate.ValidateName(input))
                {
                    Console.Write("Book Author: ");
                    input = Console.ReadLine();
                }
                book.author = input;

                //get publisher from user
                Console.Write("Book's Publisher: ");
                input = Console.ReadLine();
                while (input == "")
                {
                    Console.Write("Publisher can not be blank, enter 'Self-Published' for non-sellout.\n" +
                                  "Book's Publisher: ");
                    input = Console.ReadLine();
                }
                book.publisher = input;

                //get page count from user
                Console.Write("Book Page Count: ");
                input = Console.ReadLine();
                while (!Validate.ValidateNumber(input))
                {
                    Console.Write("Book Page Count: ");
                    input = Console.ReadLine();
                }
                book.pageCount = UInt16.Parse(input);

                //get list of genres from user
                do
                {
                    Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                  "Book Genre: ");
                    input = Console.ReadLine();
                    while (!Validate.ValidateWord(input))
                    {
                        Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                      "Book Genre: ");
                        input = Console.ReadLine();
                    }
                    if (!input.ToLower().Equals("end"))
                    {
                        book.genres.Add(input);
                    }
                } while (!input.ToLower().Equals("end"));

                
                //Write to file
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{book.mediaId},{book.title},{string.Join("|", book.genres)},{book.author},{book.publisher},{book.pageCount}");
                sw.Close();

                // add book details to Lists
                Books.Add(book);

                // log transaction
                logger.Info("Book id {Id} added", book.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
        
        public void SearchBooks()
        {
            Console.WriteLine("Title)\t");

            string criteria = Console.ReadLine();

            var searchResults = Books.Where(b => b.title.ToLower().Contains(criteria.ToLower()));
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
                        foreach (Book b in searchResults)
                        {
                            Console.WriteLine(b.Display());
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
