using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restAPItest
{
    public class LyricsModel
    {
        public string lyrics { get; set; }

        public int countWords()
        {
            int wordCount = 0, index = 0;

            // skip whitespace until first word
            while (index < lyrics.Length && char.IsWhiteSpace(lyrics[index]))
                index++;

            while (index < lyrics.Length)
            {
                // check if current char is part of a word
                while (index < lyrics.Length && !char.IsWhiteSpace(lyrics[index]))
                    index++;
                wordCount++;

                // skip whitespace until next word
                while (index < lyrics.Length && char.IsWhiteSpace(lyrics[index]))
                    index++;
            }
            return wordCount;
        }
    }
}
