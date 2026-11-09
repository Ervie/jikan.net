using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.TopTests
{
	[Collection("TenraiTests")]
	public class GetTopEpisodesAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetTopEpisodesAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetTopEpisodesAsync_ByScore_HasParentAnime()
		{
			var episodes = await _tenrai.GetTopEpisodesAsync(new TopEpisodesConfig { OrderBy = TopEpisodesOrderBy.Score });

			using var _ = new AssertionScope();
			episodes.Data.Should().NotBeEmpty();
			episodes.Data.Select(x => x.Score).Should().BeInDescendingOrder();
			episodes.Data.Should().OnlyContain(x => x.Entry.MalId > 0 && x.Entry.Title != null);
			episodes.Data.Should().OnlyContain(x => x.Votes > 0);
		}
	}
}
