using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests
{
	[Collection("TenraiTests")]
	public class GetAnimeRelationsAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetAnimeRelationsAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeRelationsAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeRelationsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}


		[Fact]
		public async Task GetAnimeRelationsAsync_BebopId_ShouldParseCowboyBebopRelations()
		{
			// When
			var bebop = await _tenrai.GetAnimeRelationsAsync(1);

			// Then
			using var _ = new AssertionScope();
			bebop.Data.Should().HaveCount(3);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Adaptation") && x.Entry.Count == 2);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Side Story") && x.Entry.Count == 2);
			bebop.Data.Should().ContainSingle(x => x.Relation.Equals("Summary") && x.Entry.Count == 1);
		}

		[Fact]
		public async Task GetAnimeRelationsAsync_Bebop_EntriesHaveMediaTypeAndImages()
		{
			// When
			var bebop = await _tenrai.GetAnimeRelationsAsync(1);

			// Then
			var entries = bebop.Data.SelectMany(r => r.Entry).ToList();
			using var _ = new AssertionScope();
			entries.Should().Contain(e => e.MalId == 5 && e.MediaType == "Movie");
			entries.Should().OnlyContain(e => e.Images != null);
		}
	}
}