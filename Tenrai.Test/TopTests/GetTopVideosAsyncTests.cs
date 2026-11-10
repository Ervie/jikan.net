using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.TopTests;

[Collection("TenraiTests")]
public class GetTopVideosAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetTopVideosAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetTopVideosAsync_Music_HasArtistMeta()
	{
		var videos = await _tenrai.GetTopVideosAsync(new TopVideosConfig { Type = TopVideoType.Music });

		var entries = videos.Data.SelectMany(x => x.Entry).ToList();

		using var _ = new AssertionScope();
		videos.Data.Select(x => x.Views).Should().BeInDescendingOrder();
		entries.Should().OnlyContain(x => x.MalId > 0 && x.VideoTitle != null);
		entries.Count(x => x.Meta?.Author != null).Should().BeGreaterThan(entries.Count / 2);
	}
}
