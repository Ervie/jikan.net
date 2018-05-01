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
		Task<Anime> GetAnime(long id);

		/// <summary>
		/// Return manga with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of manga.</param>
		Task<Manga> GetManga(long id);

		/// <summary>
		/// Return character with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of character.</param>
		Task<Character> GetCharacter(long id);

		/// <summary>
		/// Return person with given MAL id.
		/// </summary>
		/// <param name="id">MAL id of person.</param>
		Task<Person> GetPerson(long id);
	}
}