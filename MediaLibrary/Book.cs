using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    class Book : Media
    {
        public string author { get; set; }
        public UInt16 pageCount { get; set; }
        public string publisher { get; set; }

        public override string Display()
        {
            return $"Id: {mediaId}\n" +
                $"Title: {title}\n" +
                $"Author: {author}\n" +
                $"Pages: {pageCount}\n" +
                $"Publisher: {publisher}\n" +
                $"Genres: {string.Join(", ", genres)}\n";
        }
    }
}
