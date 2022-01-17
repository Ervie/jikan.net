using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetTopMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetTopMangaAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetTopMangaAsync_NoParameter_ShouldParseTopManga()
		{
			// When
			var top = await _jikan.GetTopMangaAsync();

			// Then
			top.Should().NotBeNull();
		}

		[Fact]
		public async Task GetTopMangaAsync_NoParameter_ShouldParseBerserk()
		{
			// When
			var top = await _jikan.GetTopMangaAsync();

			// Then
			using var _ = new AssertionScope();
			top.Data.First().Title.Should().Be("Berserk");
			top.Data.First().Publishing.Should().BeFalse();
			top.Data.First().Type.Should().Be("Manga");
			top.Data.First().Rank.Should().Be(1);
			top.Data.First().Authors.Should().ContainSingle().Which.Name.Should().Be("Miura, Kentarou");
			top.Data.First().Serializations.Should().ContainSingle().Which.Name.Should().Be("Young Animal");
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetTopMangaAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopMangaAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopMangaAsync_SecondPage_ShouldParseSecondPage()
		{
			// When
			var top = await _jikan.GetTopMangaAsync(2);

			// Then
			var titles = top.Data.Select(x => x.Title);
			using var _ = new AssertionScope();
			titles.Should().Contain("Made in Abyss");
			titles.Should().Contain("Mushishi");
			titles.Should().Contain("Nana");
		}
	}
}