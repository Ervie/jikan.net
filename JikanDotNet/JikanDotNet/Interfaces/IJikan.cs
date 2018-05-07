using System.Threading.Tasks;

namespace JikanDotNet
{
	/// <summary>
	/// Interface for Jikan.net client
	/// </summary>
	public interface IJikan
	{
		/// <summary>
		/// Return anime with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of anime.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Anime with given MAL id.</returns>
		Task<Anime> GetAnime(long id, AnimeExtension extension = AnimeExtension.None);

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Manga with given MAL id.</returns>
		Task<Manga> GetManga(long id, MangaExtension extension = MangaExtension.None);

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Character with given MAL id.</returns>
		Task<Character> GetCharacter(long id, CharacterExtension extension = CharacterExtension.None);

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		/// <param name="extension">Extension for extra data.</param>
		/// <returns>Person with given MAL id.</returns>
		Task<Person> GetPerson(long id, PersonExtension extension = PersonExtension.None);

		/// <summary>
		/// Return current season preview.
		/// </summary>
		/// <returns>Current season preview.</returns>
		Task<Season> GetSeason();

		/// <summary>
		/// Return season preview.
		/// </summary>
		/// <param name="year">Year of selected season.</param>
		/// <param name="season">Selected season.</param>
		/// <returns>Season preview.</returns>
		Task<Season> GetSeason(int year, Seasons season);

		/// <summary>
		/// Return current season schedule.
		/// </summary>
		/// <returns>Current season schedule.</returns>
		Task<Schedule> GetSchedule();

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop();

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Page of 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<AnimeTop> GetAnimeTop(int page, TopAnimeExtension extension = TopAnimeExtension.None);

		/// <summary>
		/// Return list of top manga.
		/// </summary>
		/// <returns>List of top manga.</returns>
		Task<MangaTop> GetMangaTop();

		/// <summary>
		/// Return list of top anime.
		/// </summary>
		/// <param name="page">Page of 50 records of top ranging (e.g. 1 will return first 50 records, 2 will return record from 51 to 100 etc.)</param>
		/// <param name="extension">Extension for specific type of ranking.</param>
		/// <returns>List of top anime.</returns>
		Task<MangaTop> GetMangaTop(int page, TopMangaExtension extension = TopMangaExtension.None);
	}
}