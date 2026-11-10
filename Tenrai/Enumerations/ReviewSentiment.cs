using System.ComponentModel;

namespace Tenrai;

/// <summary>
/// Sentiment classification a review can be filtered by.
/// </summary>
public enum ReviewSentiment
{
	/// <summary>
	/// Reviews recommending the entry.
	/// </summary>
	[Description("recommended")]
	Recommended,

	/// <summary>
	/// Reviews expressing mixed feelings about the entry.
	/// </summary>
	[Description("mixed_feelings")]
	MixedFeelings,

	/// <summary>
	/// Reviews not recommending the entry.
	/// </summary>
	[Description("not_recommended")]
	NotRecommended
}
