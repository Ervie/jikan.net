using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// A user recommendation from the top recommendations list, with its vote count.
/// </summary>
public class TopRecommendation : UserRecommendation
{
	/// <summary>
	/// Url of the recommendation page on MyAnimeList.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; }

	/// <summary>
	/// Number of users who agreed with the recommendation.
	/// </summary>
	[JsonPropertyName("votes")]
	public int Votes { get; set; }
}
