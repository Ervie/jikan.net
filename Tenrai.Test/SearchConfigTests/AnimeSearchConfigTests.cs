using System;
using System.Globalization;
using FluentAssertions;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class AnimeSearchConfigTests
{
	[Fact]
	public void ConfigToString_QueryWithHash_IsEscaped()
	{
		var config = new AnimeSearchConfig { Query = "C#" };

		config.ConfigToString().Should().Be("?q=C%23");
	}

	[Fact]
	public void ConfigToString_QueryWithAmpersand_IsEscaped()
	{
		var config = new AnimeSearchConfig { Query = "Tom & Jerry", Sfw = false };

		config.ConfigToString().Should().Be("?q=Tom%20%26%20Jerry");
	}

	[Fact]
	public void ConfigToString_FractionalScore_UsesInvariantCulture()
	{
		var original = CultureInfo.CurrentCulture;
		try
		{
			CultureInfo.CurrentCulture = new CultureInfo("de-DE");
			var config = new AnimeSearchConfig { MinimumScore = 7.5, Sfw = false };

			config.ConfigToString().Should().Be("?min_score=7.5");
		}
		finally
		{
			CultureInfo.CurrentCulture = original;
		}
	}

	[Fact]
	public void ConfigToString_Limit100_IsAccepted()
	{
		var config = new AnimeSearchConfig { Limit = 100, Sfw = false };

		config.ConfigToString().Should().Be("?limit=100");
	}

	[Fact]
	public void ConfigToString_Limit101_Throws()
	{
		var config = new AnimeSearchConfig { Limit = 101 };

		config.Invoking(c => c.ConfigToString()).Should().ThrowExactly<TenraiValidationException>();
	}

	[Fact]
	public void ConfigToString_SfwStrict_IsSent()
	{
		var config = new AnimeSearchConfig { Sfw = false, SfwStrict = true };

		config.ConfigToString().Should().Be("?sfw-strict=true");
	}

	[Fact]
	public void ConfigToString_EmptyGenres_AreOmitted()
	{
		var config = new AnimeSearchConfig { Genres = Array.Empty<AnimeGenreSearch>(), Sfw = false };

		config.ConfigToString().Should().BeEmpty();
	}

	[Fact]
	public void ConfigToString_Genres_AreCommaJoined()
	{
		var config = new AnimeSearchConfig { Genres = new[] { AnimeGenreSearch.Action, AnimeGenreSearch.Drama }, Sfw = false };

		config.ConfigToString().Should().Be("?genres=1,8");
	}

	[Fact]
	public void ConfigToString_ListFilters_AreSent()
	{
		var config = new AnimeSearchConfig
		{
			Types = new[] { AnimeType.TV, AnimeType.TvSpecial },
			ExcludedRatings = new[] { AnimeAgeRating.RX },
			VoiceLanguages = new[] { VoiceLanguage.English },
			Seasons = new[] { Season.Fall },
			SeasonYears = new[] { 2025 }
		};

		config.ConfigToString().Should().Be("?type=TV,tv_special&rating_exclude=rx&voice_language=english&season=fall&season_year=2025");
	}
}
