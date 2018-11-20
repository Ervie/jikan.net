using JikanDotNet;
using System;

namespace Example.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
			// Initialize JikanWrapper
			IJikan jikan = new Jikan(true);

			// Send request for "Cowboy Bebop" anime
			Anime cowboyBebop = jikan.GetAnime(1).Result;

			// Output -> "Cowboy Bebop"
			Console.WriteLine(cowboyBebop.Title);
			// Output -> "TV"
			Console.WriteLine(cowboyBebop.Type);
			// Output -> "R - 17+ (violence & profanity)"
			Console.WriteLine(cowboyBebop.Rating);


			// Send request for "Berserk" manga
			Manga berserkManga = jikan.GetManga(2).Result;

			// Output -> "Berserk"
			Console.WriteLine(berserkManga.Title);
			// Output -> "Publishing"
			Console.WriteLine(berserkManga.Status);


			// Send request for Hayao Miyazaki
			Person hayaoMiyazaki = jikan.GetPerson(1870).Result;

			// List Miyazaki anime on output
			foreach (var staffPosition in hayaoMiyazaki.AnimeStaffPositions)
			{
				Console.WriteLine("Anime: " + staffPosition.Anime.Name + ", role: " + staffPosition.Position);
			}


			// Send request for Lain Iwakura
			Character lainIwakura = jikan.GetCharacter(2219).Result;

			// List Lain's voice actresses with their respective languages
			foreach (var voiceActor in lainIwakura.VoiceActors)
			{
				Console.WriteLine("Name: " + voiceActor.Name + ", language: " + voiceActor.Language);
			}

			// List all anime in which Lain appeared
			foreach (var anime in lainIwakura.Animeography)
			{
				Console.WriteLine("Title: " + anime.Name);
			}
		}
	}
}
