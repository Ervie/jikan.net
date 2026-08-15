namespace Tenrai.Consts
{
	/// <summary>
	/// Endpoint sections in Tenrai REST API.
	/// </summary>
	internal static class TenraiEndpointConsts
	{
		internal const string Anime = "anime";
		internal const string Manga = "manga";
		internal const string Characters = "characters";
		internal const string People = "people";
		internal const string Seasons = "seasons";
		internal const string Schedules = "schedules";
		internal const string TopList = "top";
		internal const string Genres = "genres";
		internal const string Producers = "producers";
		internal const string Magazines = "magazines";
		internal const string Users = "users";
		internal const string Clubs = "clubs";
		internal const string Reviews = "reviews";
		internal const string Episodes = "episodes";
		internal const string Staff = "staff";
		internal const string Pictures = "pictures";
		internal const string Videos = "videos";
		internal const string Statistics = "statistics";
		internal const string News = "news";
		internal const string Forum = "forum";
		internal const string MoreInfo = "moreinfo";
		internal const string Recommendations = "recommendations";
		internal const string UserUpdates = "userupdates";
		internal const string Themes = "themes";
		internal const string Relations = "relations";
		internal const string Voices = "voices";
		internal const string Members = "members";
		internal const string Upcoming = "upcoming";
		internal const string History = "history";
		internal const string Friends = "friends";
		internal const string AnimeList = "animelist";
		internal const string MangaList = "mangalist";
		internal const string Favorites = "favorites";
		internal const string About = "about";
		internal const string Random = "random";
		internal const string Watch = "watch";
		internal const string Popular  = "popular";
		internal const string Promos = "promos";
		internal const string External = "external";
		internal const string Streaming = "streaming";
		internal const string Full = "full";
		internal const string Now = "now";
		internal const string Ids = "ids";
		internal const string Stacks = "stacks";

		/// <summary>
		/// Absolute URL. The status service is hosted on tenrai.org, not api.tenrai.org, so this
		/// must not be resolved against the client's BaseAddress.
		/// </summary>
		internal const string StatusEndpoint = "https://tenrai.org/status/api/status";
	}
}