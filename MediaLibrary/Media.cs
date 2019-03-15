using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    abstract class Media
    {
        // public properties
        public UInt64 mediaId { get; set; }
        public string title { get; set; }
        public List<string> genres { get; set; }

        // constructor
        public Media()
        {
            genres = new List<string>();
        }

        // public method
        public virtual string Display()
        {
            return $"Id: {mediaId}\n" +
                $"Title: {title}\n" +
                $"Genres: {string.Join(", ", genres)}\n";
        }
    }
}
