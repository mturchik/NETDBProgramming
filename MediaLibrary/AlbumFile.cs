using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MediaLibrary
{
    class AlbumFile
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public string filePath { get; set; }
        public List<Album> Albums { get; set; }

        public AlbumFile(string path)
        {
            Albums = new List<Album>();
            filePath = path;
            //populate list with new movie objects
            try
            {
                StreamReader sr = new StreamReader(filePath);
                while (!sr.EndOfStream)
                {
                    //create new movie
                    Album album = new Album();
                    //get album line
                    string line = sr.ReadLine();
                    //get album ID by splicing out from first comma
                    album.mediaId = UInt64.Parse(line.Substring(0, line.IndexOf(',')));
                    //remove info from string
                    line = line.Remove(0, line.IndexOf(',') + 1);

                    //Get label name from end of string
                    album.recordLabel = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //get artist info from string
                    album.artist = line.Substring(line.LastIndexOf(',') + 1);
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //get Genres from string
                    //break genres down along | and pass array to album
                    string[] genres = line.Substring(line.LastIndexOf(',') + 1).Split('|');
                    foreach (string genre in genres)
                    {
                        album.genres.Add(genre);
                    }
                    //remove info from string
                    line = line.Remove(line.LastIndexOf(','));

                    //add movie title to last field of movie
                    album.title = line;

                    //add movie to list
                    Albums.Add(album);
                }
                sr.Close();
                logger.Info("Movies in file {Count}", Albums.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        //check if title input is already in file
        public bool isUniqueTitle(string title)
        {
            if (Albums.ConvertAll(a => a.title.ToLower()).Contains(title.ToLower()))
            {
                logger.Info("Duplicate album title {Title}", title);
                return false;
            }
            return true;
        }

        public void DisplayAlbums()
        {
            foreach (Album a in Albums)
            {
                Console.WriteLine(a.Display());
            }
        }

        public void AddAlbum()
        {
            try
            {
                //title, genres, artist, record label
                Album album = new Album();

                // first generate movie id
                album.mediaId = Albums.Max(a => a.mediaId) + 1;

                //get title from user
                Console.Write("Album Title: ");
                string title = Console.ReadLine();
                while (!isUniqueTitle(title))
                {
                    Console.Write("Duplicate album title...\n" +
                                  "Album Title: ");
                    title = Console.ReadLine();
                }
                // if title contains a comma, wrap it in quotes
                title = title.IndexOf(',') != -1 ? $"\"{title}\"" : title;


                //get name of artist from user
                Console.Write("Album Artist: ");
                string artist = Console.ReadLine();
                while (!Validate.ValidateName(artist))
                {
                    Console.Write("Album Artist: ");
                    artist = Console.ReadLine();
                }
                album.artist = artist;

                //get record label from user
                Console.Write("Album's Record Label: ");
                string label = Console.ReadLine();
                while (label == "")
                {
                    Console.Write("Label can not be blank, enter 'Self-Published' for non-sellout.\n" +
                                  "Album's Record Label: ");
                    label = Console.ReadLine();
                }
                album.recordLabel = label;

                //get list of genres from user
                string genre;
                do
                {
                    Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                  "Album Genre: ");
                    genre = Console.ReadLine();
                    while (!Validate.ValidateWord(genre))
                    {
                        Console.Write("Enter Genres, enter 'End' to move on.\n" +
                                      "Album Genre: ");
                        genre = Console.ReadLine();
                    }
                    if (!genre.ToLower().Equals("end"))
                    {
                        album.genres.Add(genre);
                    }
                } while (!genre.ToLower().Equals("end"));

                //Write to file
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine($"{album.mediaId},{title},{string.Join("|", album.genres)},{album.artist},{album.recordLabel}");
                sw.Close();

                // add movie details to Lists
                Albums.Add(album);

                // log transaction
                logger.Info("Album id {Id} added", album.mediaId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public void SearchAlbums()
        {
            Console.WriteLine("Title)\t");

            string criteria = Console.ReadLine();

            var searchResults = Albums.Where(a => a.title.ToLower().Contains(criteria.ToLower()));
            Console.WriteLine($"There are {searchResults.Count()} albums that contain \"{criteria}\"\n" +
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
                        foreach (Album a in searchResults)
                        {
                            Console.WriteLine(a.Display());
                        }
                        Console.WriteLine($"Results: {searchResults.Count()} albums");
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
