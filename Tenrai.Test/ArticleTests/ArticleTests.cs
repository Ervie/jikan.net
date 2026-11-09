using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.ArticleTests
{
	[Collection("TenraiTests")]
	public class ArticleTests
	{
		private readonly ITenrai _tenrai;

		public ArticleTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetArticlesAsync_FirstPage_HasTotal()
		{
			// When
			var articles = await _tenrai.GetArticlesAsync();

			// Then
			using var _ = new AssertionScope();
			articles.Data.Should().HaveCount(25);
			articles.Pagination.Items.Total.Should().BeGreaterThan(1000);
		}

		[Fact]
		public async Task GetArticleAsync_2422_HasTagsAndViews()
		{
			// When
			var article = await _tenrai.GetArticleAsync(2422);

			// Then
			using var _ = new AssertionScope();
			article.Data.Title.Should().Be("The Secret to Finding Rare Japanese Anime Merch");
			article.Data.Tags.Should().Contain("Advertorial");
			article.Data.Views.Should().BeGreaterThan(0);
			article.Data.Content.Html.Should().NotBeNullOrEmpty();
			article.Data.Relations.Characters.Should().NotBeNull();
		}

		[Fact]
		public async Task GetArticleTagsAsync_ContainsSpoiler()
		{
			// When
			var tags = await _tenrai.GetArticleTagsAsync();

			// Then
			tags.Data.Should().Contain(x => x.Name == "Spoiler" && x.Count > 0);
		}

		[Fact]
		public async Task GetAnimeArticlesAsync_OnePiece_NotEmpty()
		{
			// When
			var articles = await _tenrai.GetAnimeArticlesAsync(21);

			// Then
			using var _ = new AssertionScope();
			articles.Data.Should().NotBeEmpty();
			articles.Pagination.HasNextPage.Should().BeTrue();
		}

		[Fact]
		public async Task GetCharacterArticlesAsync_Luffy_NotEmpty()
		{
			// When
			var articles = await _tenrai.GetCharacterArticlesAsync(40);

			// Then
			articles.Data.Should().NotBeEmpty();
		}

		[Fact]
		public async Task GetPersonArticlesAsync_NoArticles_IsEmpty()
		{
			// When
			var articles = await _tenrai.GetPersonArticlesAsync(1);

			// Then
			articles.Data.Should().BeEmpty();
		}
	}
}
