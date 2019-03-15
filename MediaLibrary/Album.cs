using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    class Album : Media
    {
        public string artist { get; set; }
        public string recordLabel { get; set; }

        public override string Display()
        {
            return $"Id: {mediaId}\n" +
                $"Title: {title}\n" +
                $"Artist: {artist}\n" +
                $"Label: {recordLabel}\n" +
                $"Genres: {string.Join(", ", genres)}\n";
        }
    }
}
