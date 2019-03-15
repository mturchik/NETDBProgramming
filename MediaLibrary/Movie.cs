using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary
{
    class Movie : Media
    {
        public string director { get; set; }
        public TimeSpan runningTime { get; set; }

        public override string Display()
        {
            return $"Id: {mediaId}\n" +
                $"Title: {title}\n" +
                $"Director: {director}\n" +
                $"Running Time: {runningTime}\n" +
                $"Genres: {string.Join(", ", genres)}\n";
        }
    }
}
