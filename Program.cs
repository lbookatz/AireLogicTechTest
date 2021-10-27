using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace restAPItest
{
    class Program
    {
        static void Main(string[] args)
        {
            string anotherArtist = "y";

            while (anotherArtist == "y" || anotherArtist == "yes")
            {
                Console.WriteLine("Type the name of the artist?");
                var nameOfArtist = Console.ReadLine();

                //gets a list of all the songs given an artist name. 
                var apiClientSongs = "http://musicbrainz.org/ws/2/";
                var apiRequestSongs = $"recording/?fmt=json&query=artist:{nameOfArtist}";                 
                var songsModel = getSongs(apiClientSongs, apiRequestSongs);
                List<string> TitlesOfSongs = new List<string>();
                foreach (var title in songsModel.recordings)
                {
                    TitlesOfSongs.Add(title.title);
                }

                //takes a the list of songs, gets the lyrics and works out the mean number of lyrics.  
                var allSongs = new ListLyrics();       
                var apiClientLyrics = "https://api.lyrics.ovh/v1/";
                allSongs.songs = new List<LyricsModel>();
                foreach (string nameSong in TitlesOfSongs)
                {
                    var lyrics = new LyricsModel();
                    lyrics = getLyrics(apiClientLyrics,$"{nameOfArtist}/{nameSong}");
                    allSongs.songs.Add(lyrics);
                }
                int mean = allSongs.MeanOfSongs();

                Console.WriteLine($"The mean words in the songs of {nameOfArtist} are {mean}");
                Console.WriteLine("Would you like to see the avarage for another Artist?\nType y to go again");
                anotherArtist = Console.ReadLine();
            }
            Console.WriteLine("Aire is great");
        }

        public static SongsModel getSongs(string cli, string req)
        {
            var client = new RestClient(cli);
            var request = new RestRequest(req);
            var response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
            SongsModel result = JsonConvert.DeserializeObject<SongsModel>(rawResponse);

            return result;
            }
            return null;
        }
        public static LyricsModel getLyrics(string cli, string req)
        {
            var client = new RestClient(cli);
            var request = new RestRequest(req);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Content;
                LyricsModel result = JsonConvert.DeserializeObject<LyricsModel>(rawResponse);

                return result;
            }
            return null;
        }


    }


}
