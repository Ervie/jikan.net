using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Tenrai.Tests.SearchConfigTests;

public class FullDataConfigTests
{
	[Fact]
	public void ConfigToString_IdsAndMode()
	{
		var config = new FullDataConfig { Ids = new List<long> { 5, 6 }, Mode = FullDataMode.Deleted };

		config.ConfigToString().Should().Be("?ids=5,6&mode=deleted");
	}
}
