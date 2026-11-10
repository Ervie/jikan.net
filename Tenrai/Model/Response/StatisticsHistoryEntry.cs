using System;
using System.Text.Json.Serialization;

namespace Tenrai;

/// <summary>
/// One day of an anime's or manga's statistics history.
/// </summary>
/// <typeparam name="TStatistics"><see cref="AnimeStatistics"/> or <see cref="MangaStatistics"/>.</typeparam>
public class StatisticsHistoryEntry<TStatistics> where TStatistics : class
{
	/// <summary>
	/// Day the numbers were taken.
	/// </summary>
	[JsonPropertyName("date")]
	public DateTime Date { get; set; }

	/// <summary>
	/// Score on that day.
	/// </summary>
	[JsonPropertyName("score")]
	public double? Score { get; set; }

	/// <summary>
	/// Number of users who scored it.
	/// </summary>
	[JsonPropertyName("scored_by")]
	public int? ScoredBy { get; set; }

	/// <summary>
	/// Rank by score.
	/// </summary>
	[JsonPropertyName("rank")]
	public int? Rank { get; set; }

	/// <summary>
	/// Rank by members.
	/// </summary>
	[JsonPropertyName("popularity")]
	public int? Popularity { get; set; }

	/// <summary>
	/// Number of users with it on their list.
	/// </summary>
	[JsonPropertyName("members")]
	public int? Members { get; set; }

	/// <summary>
	/// Number of users who favorited it.
	/// </summary>
	[JsonPropertyName("favorites")]
	public int? Favorites { get; set; }

	/// <summary>
	/// List status and score breakdown. Null on days before 2026-04-04 and for titles under 1000 members.
	/// </summary>
	[JsonPropertyName("statistics")]
	public TStatistics Statistics { get; set; }
}
