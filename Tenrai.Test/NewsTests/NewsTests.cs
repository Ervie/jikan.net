using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.NewsTests
{
	[Collection("TenraiTests")]
	public class NewsTests
	{
		private readonly ITenrai _tenrai;

		public NewsTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetNewsAsync_FirstPage_HasTotal()
		{
			// When
			var news = await _tenrai.GetNewsAsync();

			// Then
			using var _ = new AssertionScope();
			news.Data.Should().HaveCount(25);
			news.Pagination.Items.Total.Should().BeGreaterThan(10000);
		}

		[Fact]
		public async Task GetNewsItemAsync_FruitsBasket_HasContentAndRelations()
		{
			// When
			var item = await _tenrai.GetNewsItemAsync(56264086);

			// Then
			using var _ = new AssertionScope();
			item.Data.Title.Should().Be("Manga 'Fruits Basket' Receives New TV Anime Adaptation");
			item.Data.Content.Html.Should().NotBeNullOrEmpty();
			item.Data.Content.Markdown.Should().NotBeNullOrEmpty();
			item.Data.Relations.Anime.Should().HaveCount(2);
			item.Data.Relations.Manga.Should().HaveCount(1);
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetNewsItemAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetNewsItemAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetNewsTagsAsync_ContainsPreview()
		{
			// When
			var tags = await _tenrai.GetNewsTagsAsync();

			// Then
			tags.Data.Should().Contain(x => x.Name == "Preview" && x.Count > 0);
		}

		[Fact]
		public async Task SearchNewsAsync_Query_ReturnsMatches()
		{
			// When
			var news = await _tenrai.SearchNewsAsync(new NewsSearchConfig { Query = "One Piece", Limit = 5 });

			// Then
			using var _ = new AssertionScope();
			news.Data.Should().HaveCount(5);
			news.Pagination.Items.Total.Should().BeGreaterThan(1000);
		}

		[Fact]
		public async Task GetAnimeNewsAsync_ByCommentsDesc_IsSorted()
		{
			// When
			var news = await _tenrai.GetAnimeNewsAsync(21, new NewsFilterConfig
			{
				OrderBy = NewsOrderBy.Comments,
				SortDirection = SortDirection.Descending
			});

			// Then
			news.Data.Select(x => x.Comments).Should().BeInDescendingOrder();
		}
	}
}
