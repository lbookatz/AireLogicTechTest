using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restAPItest
{
    public class ListLyrics
    {
        public List<LyricsModel> songs { get; set; }

        public int MeanOfSongs()
        {
            int TotalWords = 0;
            int TotalSongs = 0;
            foreach (LyricsModel song in songs)
            {
                if (song is not null)
                    if (song.lyrics is not null)
                    {
                        TotalWords += song.countWords();
                        TotalSongs++;
                        Console.WriteLine(song.lyrics);
                        Console.WriteLine(TotalSongs);
                        Console.WriteLine();
                    }
            }
            var result = TotalSongs == 0 ? 0 : TotalWords / TotalSongs;
            return result;
        }
    }
}
