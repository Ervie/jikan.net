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


			// Send request for manga ranking (highest rating).
			MangaTop topMangaList = jikan.GetMangaTop().Result;

			// Print title of each manga in the top 50
			foreach (var listEntry in topMangaList.Top)
			{
				Console.WriteLine(listEntry.Title);
			}

			// Send request for second page (positions 51-100) of manga with highest ratings.
			topMangaList = jikan.GetMangaTop(2).Result;

			// Send request for light novels with highest ratings.
			topMangaList = jikan.GetMangaTop(TopMangaExtension.TopNovel).Result;

			// Send request for second page of most popular manga.
			topMangaList = jikan.GetMangaTop(2, TopMangaExtension.TopPopularity).Result;

			// Send request for most popular people on MAL
			PeopleTop topPeopleList = jikan.GetPeopleTop().Result;

			// Print rank and name of each person in top 50.
			foreach (var listEntry in topPeopleList.Top)
			{
				Console.WriteLine(listEntry.Rank + ". " + listEntry.Name);
			}

			// Send request for second page (positions 51 - 100) of most popular people
			topPeopleList = jikan.GetPeopleTop(2).Result;

			// Send request for most popular characters on MAL
			CharactersTop topCharacterList = jikan.GetCharactersTop().Result;

			// Print rank and name of each character in top 50.
			foreach (var listEntry in topCharacterList.Top)
			{
				Console.WriteLine(listEntry.Rank + ". " + listEntry.Name);
			}

			// Send request for second page (positions 51 - 100) of most popular characters.
			topCharacterList = jikan.GetCharactersTop(2).Result;

			// Send request for mecha genre (and first 100 mecha anime snippets)
			AnimeGenre animeGenre = jikan.GetAnimeGenre(18).Result;

			// Output -> "Mecha"
			Console.WriteLine(animeGenre.Metadata.Name);

			// Print title of each of the first 100 mecha anime
			foreach (var anime in animeGenre.Anime)
			{
				Console.WriteLine(anime.Title);
			}

			// Send request for mecha genre (mecha anime snippets on positions 101-200)
			animeGenre = jikan.GetAnimeGenre(18, 2).Result;

			// Send request for samurai anime genre (and first 100 samurai anime snippets)
			animeGenre = jikan.GetAnimeGenre(GenreSearch.Samurai).Result;

			// Send request for sports genre (sports anime snippets on positions 201-300)
			animeGenre = jikan.GetAnimeGenre(GenreSearch.Sports, 3).Result;

			// Send request for mecha genre (and first 100 mecha manga snippets)
			MangaGenre mangaGenre = jikan.GetMangaGenre(18).Result;

			// Output -> "Mecha"
			Console.WriteLine(mangaGenre.Metadata.Name);

			// Print title of each of the first 100 mecha manga
			foreach (var anime in mangaGenre.Manga)
			{
				Console.WriteLine(anime.Title);
			}

			// Send request for mecha genre (mecha manga snippets on positions 101-200)
			mangaGenre = jikan.GetMangaGenre(18, 2).Result;

			// Send request for samurai manga genre (and first 100 samurai manga snippets)
			mangaGenre = jikan.GetMangaGenre(GenreSearch.Samurai).Result;

			// Send request for sports genre (sports manga snippets on positions 201-300)
			mangaGenre = jikan.GetMangaGenre(GenreSearch.Sports, 3).Result;

			// Send request for KyotoAnimation
			Producer producer = jikan.GetProducer(2).Result;

			// Output -> "Kyoto Animation"
			Console.WriteLine(producer.Metadata.Name);

			// Print title of each anime of the first 100 anime made by Kyoto Animation
			foreach (var anime in producer.Anime)
			{
				Console.WriteLine(anime.Title);
			}
			
			// Send request for "Studio Pierrot" (and their anime listed on positions 101-200)
			producer = jikan.GetProducer(1, 2).Result;

			// Send request for "Young Animal" magazine.
			Magazine magazine = jikan.GetMagazine(2).Result;

			// Output -> "Young Animal"
			Console.WriteLine(magazine.Metadata.Name);

			// Print title of each manga of the first 100 mangas of "Young Animal"
			foreach (var manga in magazine.Manga)
			{
				Console.WriteLine(manga.Title);
			}

			// Send request for "Shonen Jump" (and their manga listed on positions 101-200)
			magazine = jikan.GetMagazine(83, 2).Result;

			UserProfile profile = jikan.GetUserProfile("Ervelan").Result;

			// Output -> "Male"
			Console.WriteLine(profile.Gender);

			// Print name of each favorite character
			foreach (var favoriteCharacter in profile.Favorites.Characters)
			{
				Console.WriteLine(favoriteCharacter.Name);
			}
			
			// Print information about completed and watching/reading anime/manga.
			Console.WriteLine("Completed " + profile.AnimeStatistics.Completed + " anime and " + profile.MangaStatistics.Completed + " manga.");
			Console.WriteLine("Currently watching " + profile.AnimeStatistics.Watching + " anime and reading " + profile.MangaStatistics.Reading + " manga.");

			// Send request for history of user "Ervelan"
			UserHistory userHistory = jikan.GetUserHistory("Ervelan").Result;

			// Print title of each anime/manga and related incerement.
			foreach (var historyEntry in userHistory.History)
			{
				Console.WriteLine(historyEntry.Metadata.Name + ": " + historyEntry.Increment);
			}

			// Send request for friend list of user "Ervelan"
			UserFriends friends = jikan.GetUserFriends("Ervelan").Result;

			// Print each friend username with their last online activity date.
			foreach (var friend in friends.Friends)
			{
				Console.WriteLine(friend.Username + " last seen online " + friend.LastOnline);
			}

			// Send request for friend list of user "batsling1234" on positions 101-200 (sorted by most recent online activity).
			friends = jikan.GetUserFriends("batsling1234", 2).Result;

			// Send request for anime list of user with "Ervelan" username (first 300 entries)
			UserAnimeList animeList = jikan.GetUserAnimeList("Ervelan").Result;

			// Print first 300 anime on requested list wiht scores assigned by user.
			foreach (var anime in animeList.Anime)
			{
				Console.WriteLine("Title: " + anime.Title + ", " + anime.Score);
			}

			// Send request for anime list of user with "Ervelan" username (entries from position 301 to 600).
			animeList = jikan.GetUserAnimeList("Ervelan", 2).Result;

			// Send request for anime list of user with "Ervelan" username (dropped anime only).
			animeList = jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Dropped).Result;


			// Send request for anime list of user with "Ervelan" username (completed anime only, from position 301 to 600).
			animeList = jikan.GetUserAnimeList("Ervelan", UserAnimeListExtension.Completed, 2).Result;

			// Send request for manga list of user with "SonMati" username (first 300 entries)
			UserMangaList mangalist = jikan.GetUserMangaList("SonMati").Result;

			// Print first 300 manga on requested list wiht scores assigned by user.
			foreach (var manga in mangalist.Manga)
			{
				Console.WriteLine("Title: " + manga.Title + ", " + manga.Score);
			}

			// Send request for manga list of user with "SonMati" username (entries from position 301 to 600).
			mangalist = jikan.GetUserMangaList("SonMati", 2).Result;

			// Send request for manga list of user with "SonMati" username (reading manga only).
			mangalist = jikan.GetUserMangaList("SonMati", UserMangaListExtension.Reading).Result;

			// Send request for manga list of user with "SonMati" username (completed manga only, from position 301 to 600).
			mangalist = jikan.GetUserMangaList("SonMati", UserMangaListExtension.Completed, 2).Result;
		}
	}
}
