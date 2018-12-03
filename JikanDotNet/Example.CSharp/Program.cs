using JikanDotNet;
using System;
using System.Linq;

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
			

			// Send request for episodes of "Cowboy Bebop" anime
			AnimeEpisodes episodes = jikan.GetAnimeEpisodes(1).Result;

			// Output -> "1"
			Console.WriteLine("Last page: " + episodes.EpisodesLastPage);

			// Print number and English title of each episode
			foreach (AnimeEpisode episode in episodes.EpisodeCollection)
			{
				Console.WriteLine("Episode " + episode.Id + ", English title: " + episode.Title);
			}

			// Send request for episodes of "One Piece" anime -> loads data of episodes from 1 to 100
			AnimeEpisodes onePieceEpisodes = jikan.GetAnimeEpisodes(21).Result;

			// Send request for episodes of "One Piece" anime -> loads data of episodes from 1 to 100
			onePieceEpisodes = jikan.GetAnimeEpisodes(21,1).Result;

			// Send request for episodes of "One Piece" anime -> loads data of episodes from 101 to 200
			onePieceEpisodes = jikan.GetAnimeEpisodes(21, 2).Result;

			// Send request for "Berserk" manga
			Manga berserkManga = jikan.GetManga(2).Result;

			// Output -> "Berserk"
			Console.WriteLine(berserkManga.Title);
			// Output -> "Publishing"
			Console.WriteLine(berserkManga.Status);

			// Send request for episodes of "Cowboy Bebop" staff and members
			AnimeCharactersStaff charactersStaff = jikan.GetAnimeCharactersStaff(1).Result;

			// Print all characters names and their role (main or supporting).
			foreach (var character in charactersStaff.Characters)
			{
				Console.WriteLine(character.Name + " (" + character.Role + ")");
			}

			// Print all staff members names and their position during production.
			foreach (var staffMember in charactersStaff.Staff)
			{
				Console.WriteLine(staffMember.Name + " (" + String.Join(", ", staffMember.Role) + ")");
			}

			// Send request for pictures of "Cowboy Bebop" 
			AnimePictures pictures = jikan.GetAnimePictures(1).Result;

			// Print link to every picture.
			foreach (Picture picture in pictures.Pictures)
			{
				Console.WriteLine(picture.Large); ;
			}

			// Send request for episodes of "Cowboy Bebop" pictures
			AnimeNews news = jikan.GetAnimeNews(1).Result;

			// Print date of each news
			foreach (News newsEntry in news.News)
			{
				Console.WriteLine(newsEntry.Date);
			}

			// Send request for videos of "Cowboy Bebop" 
			AnimeVideos videos = jikan.GetAnimeVideos(1).Result;

			// Print each episode video title
			foreach (var episode in videos.EpisodeVideos)
			{
				Console.WriteLine("Episode " + episode.NumberedTitle + ": " + episode.Title);
			}

			// Print each promo video title
			foreach (var promo in videos.PromoVideos)
			{
				Console.WriteLine(promo.Title);
			}

			// Send request for statistics of "Cowboy Bebop" 
			AnimeStats stats = jikan.GetAnimeStatistics(1).Result;

			// Print statistics to output.
			Console.WriteLine("Comppleted by " + stats.Completed + " users");
			Console.WriteLine("Being watched by " + stats.Watching + " users");
			Console.WriteLine("Dropped by " + stats.Dropped + " users");

			// Send request for forum topics of "Cowboy Bebop"
			ForumTopics forumTopics = jikan.GetAnimeForumTopics(1).Result;

			// Post each topic title and date of creation
			foreach (var topic in forumTopics.Topics)
			{
				Console.WriteLine(topic.Title + ", created:" + topic.DatePosted);
			}

			// Send request for more info of "Cowboy Bebop"
			MoreInfo moreInfo = jikan.GetAnimeMoreInfo(1).Result;

			// Output -> "Suggested Order..."
			Console.WriteLine("Additional info:" + moreInfo.Info);

			// Send request for pictures of "Berserk" manga. 
			MangaPictures berserkPics = jikan.GetMangaPictures(2).Result;

			// Print link to every picture.
			foreach (Picture picture in pictures.Pictures)
			{
				Console.WriteLine(picture.Large); ;
			}

			// Send request for "Berserk" characters
			MangaCharacters berserkCharacters = jikan.GetMangaCharacters(1).Result;

			// Print all characters names and their role (main or supporting).
			foreach (var character in berserkCharacters.Characters)
			{
				Console.WriteLine(character.Name + " (" + character.Role + ")");
			}

			// Send request for "Berserk" news
			MangaNews berserjNews = jikan.GetMangaNews(2).Result;

			// Print date of each news
			foreach (News newsEntry in news.News)
			{
				Console.WriteLine(newsEntry.Date);
			}

			// Send request for statistics of "Berserk" manga 
			MangaStats berserkStats = jikan.GetMangaStatistics(2).Result;

			// Print statistics to output.
			Console.WriteLine("Completed by " + berserkStats.Completed + " users");
			Console.WriteLine("Being read by " + berserkStats.Reading + " users");
			Console.WriteLine("Dropped by " + berserkStats.Dropped + " users");

			// Send request for forum topics of "Berserk" manga.
			ForumTopics bersekrForumTopics = jikan.GetMangaForumTopics(2).Result;

			// Post each topic title and date of creation
			foreach (var topic in bersekrForumTopics.Topics)
			{
				Console.WriteLine(topic.Title + ", created:" + topic.DatePosted);
			}

			// Send request for more info of "Berserk" manga.
			MoreInfo berserkMoreInfo = jikan.GetMangaMoreInfo(2).Result;

			// Output -> "Berserk: The Prototype (1988)"
			Console.WriteLine("Additional info:" + moreInfo.Info);

			// Send request for Hayao Miyazaki
			Person hayaoMiyazaki = jikan.GetPerson(1870).Result;

			// List Miyazaki anime on output
			foreach (var staffPosition in hayaoMiyazaki.AnimeStaffPositions)
			{
				Console.WriteLine("Anime: " + staffPosition.Anime.Name + ", role: " + staffPosition.Position);
			}

			// Send request for pictures of Hayao Miyazaki.
			PersonPictures hayaoMiyazakiPics = jikan.GetPersonPictures(1870).Result;

			// Print link to every picture.
			foreach (Picture picture in hayaoMiyazakiPics.Pictures)
			{
				Console.WriteLine(picture.Large); ;
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

			// Send request for pictures of Spike Spiegel.
			CharacterPictures spikePics = jikan.GetCharacterPictures(1).Result;

			// Print link to every picture.
			foreach (Picture picture in spikePics.Pictures)
			{
				Console.WriteLine(picture.Large); ;
			}

			// Send request for current season.
			Season season = jikan.GetSeason().Result;

			// Print season basic information
			Console.WriteLine("Season : " + season.SeasonYear + " " + season.SeasonName);

			// Print each anime title of the season.
			foreach (var seasonEntry in season.SeasonEntries)
			{
				Console.WriteLine(seasonEntry.Title);
			}

			// Send request for Fall 2010
			season = jikan.GetSeason(2010, Seasons.Fall).Result;

			// Send request for season archives
			SeasonArchives seasonArchive = jikan.GetSeasonArchive().Result;

			// Print all available years with their available seasons
			foreach (var archive in seasonArchive.Archives)
			{
				Console.WriteLine("Year: " + archive.Year + ", available seasons: " + string.Join(", ", archive.Season));
			}

			// Send request for schedule
			Schedule schedule = jikan.GetSchedule().Result;

			// Print title of each anime airing on monday
			foreach (var mondayAnime in schedule.Monday)
			{
				Console.WriteLine(mondayAnime.Title);
			}

			// Print title of each anime with irregular airing cycle.
			foreach (var other in schedule.Other)
			{
				Console.WriteLine(other.Title);
			}

			// Send request for Sunday schedule
			schedule = jikan.GetSchedule(ScheduledDay.Sunday).Result;

			// Print title of each anime airing on monday
			foreach (var sundayAnime in schedule.Sunday)
			{
				Console.WriteLine(sundayAnime.Title);
			}

			// Will throw Exception -> schedule.Monday is null!
			foreach (var mondayAnime in schedule.Monday)
			{
				Console.WriteLine(mondayAnime.Title);
			}

			// Send request for anime ranking (highest rating).
			AnimeTop topAnimeList = jikan.GetAnimeTop().Result;

			// Print title of each anime in the top 50
			foreach (var listEntry in topAnimeList.Top)
			{
				Console.WriteLine(listEntry.Title);
			}

			// Send request for second page (positions 51-100) of anime with highest ratings.
			topAnimeList = jikan.GetAnimeTop(2).Result;

			// Send request for anime with highest ratings currently airing.
			topAnimeList = jikan.GetAnimeTop(TopAnimeExtension.TopAiring).Result;

			// Send request for second page of most popular anime.
			topAnimeList = jikan.GetAnimeTop(2, TopAnimeExtension.TopPopularity).Result;
		}
	}
}
