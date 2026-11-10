using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class MangaSearchConfigTests
{
	[Fact]
	public void ConfigToString_Seinen_SendsGenre41()
	{
		var config = new MangaSearchConfig { Genres = new[] { MangaGenreSearch.Seinen } };

		config.ConfigToString().Should().Be("?genres=41");
	}

	[Fact]
	public void ConfigToString_NovelAndLightNovel_SendDistinctTypes()
	{
		var config = new MangaSearchConfig { Types = new[] { MangaType.Novel, MangaType.LightNovel } };

		config.ConfigToString().Should().Be("?type=novel,lightnovel");
	}
}
