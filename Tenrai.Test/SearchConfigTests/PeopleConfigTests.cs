using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class PeopleConfigTests
{
	[Fact]
	public void CharacterSearchConfig_BebopMainCastByFavorites_BuildsQuery()
	{
		var config = new CharacterSearchConfig
		{
			AnimeIds = { 1 },
			Role = CharacterRole.Main,
			OrderBy = CharacterSearchOrderBy.Favorites,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?anime=1&role=main&order_by=favorites&sort=desc");
	}

	[Fact]
	public void ProducerSearchConfig_StudiosActiveSince2020ByCount_BuildsQuery()
	{
		var config = new ProducerSearchConfig
		{
			Roles = { ProducerRole.Studio },
			ActiveAfter = 2020,
			OrderBy = ProducerSearchOrderBy.Count,
			SortDirection = SortDirection.Descending
		};

		config.ConfigToString().Should().Be("?role=studio&active_after=2020&order_by=count&sort=desc");
	}
}
