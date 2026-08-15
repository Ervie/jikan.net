using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.InterestStackTests
{
	[Collection("TenraiTests")]
	public class GetInterestStackAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetInterestStackAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetInterestStackAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetInterestStackAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetInterestStackAsync_FirstBrowsedStack_ShouldParseEntriesInSourceOrder()
		{
			// Given
			var browsed = await _tenrai.GetInterestStacksAsync();
			var id = browsed.Data.First().MalId;

			// When
			var stack = await _tenrai.GetInterestStackAsync(id);

			// Then
			using (new AssertionScope())
			{
				stack.Data.MalId.Should().Be(id);
				stack.Data.Url.Should().NotBeNullOrWhiteSpace();
				stack.Data.Entries.Should().NotBeNullOrEmpty();
				stack.Data.Entries.Count.Should().BeLessOrEqualTo(stack.Data.EntryCount);
				stack.Data.Entries.Select(entry => entry.Position).Should().BeInAscendingOrder();
				stack.Data.Entries.First().Position.Should().Be(1);
				stack.Data.Entries.Should().OnlyContain(entry => entry.MalId > 0);
				stack.Data.Entries.Should().OnlyContain(entry => !string.IsNullOrWhiteSpace(entry.Title));
				stack.Data.Entries.Should().OnlyContain(entry => !string.IsNullOrWhiteSpace(entry.Url));
				stack.Data.Entries.Should().OnlyContain(entry => !string.IsNullOrWhiteSpace(entry.Type));
				stack.Data.Entries.Should().OnlyContain(entry => entry.Images.JPG.ImageUrl != null);
			}
		}

		[Fact]
		public async Task GetInterestStackAsync_AnimeStack_ShouldParseAnimeSpecificEntryFields()
		{
			// Given
			var browsed = await _tenrai.SearchInterestStacksAsync(new InterestStackSearchConfig { Type = InterestStackType.Anime });
			var id = browsed.Data.First().MalId;

			// When
			var stack = await _tenrai.GetInterestStackAsync(id);

			// Then
			using (new AssertionScope())
			{
				stack.Data.StackType.Should().Be("anime");
				stack.Data.Entries.Should().Contain(entry => entry.AiredFromYear.HasValue);
				stack.Data.Entries.Should().OnlyContain(entry => entry.Volumes == null);
				stack.Data.Entries.Should().OnlyContain(entry => entry.PublishedFromYear == null);
			}
		}

		[Fact]
		public async Task GetInterestStackAsync_MangaStack_ShouldParseMangaSpecificEntryFields()
		{
			// Given
			var browsed = await _tenrai.SearchInterestStacksAsync(new InterestStackSearchConfig { Type = InterestStackType.Manga });
			var id = browsed.Data.First().MalId;

			// When
			var stack = await _tenrai.GetInterestStackAsync(id);

			// Then
			using (new AssertionScope())
			{
				stack.Data.StackType.Should().Be("manga");
				stack.Data.Entries.Should().Contain(entry => entry.PublishedFromYear.HasValue);
				stack.Data.Entries.Should().OnlyContain(entry => entry.Episodes == null);
				stack.Data.Entries.Should().OnlyContain(entry => entry.AiredFromYear == null);
			}
		}
	}
}
