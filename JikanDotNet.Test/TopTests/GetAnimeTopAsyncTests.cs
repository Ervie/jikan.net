using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetAnimeTopAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeTopAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetAnimeTopAsync_NoParameter_ShouldParseTopAnime()
		{
			// When
			var top = await _jikan.GetAnimeTopAsync();

			top.Should().NotBeNull();
		}

		[Fact]
		public async Task GetAnimeTopAsync_NoParameter_ShouldParseFMA()
		{
			// When
			var top = await _jikan.GetAnimeTopAsync();

			// Then
			top.Data.First().Title.Should().Be("Fullmetal Alchemist: Brotherhood");
			top.Data.First().Episodes.Should().Be(64);
			top.Data.First().Source.Should().Be("Manga");
			top.Data.First().Duration.Should().Be("24 min per ep");
			top.Data.First().Producers.Should().HaveCount(4);
			top.Data.First().Licensors.Should().HaveCount(2);
			top.Data.First().Studios.Should().ContainSingle().Which.Name.Should().Be("Bones");
		}

		[Fact]
		public async Task GetAnimeTopAsync_NoParameter_ShouldParseLOGHType()
		{
			// When
			var top = await _jikan.GetAnimeTopAsync();

			// Then
			var logh = top.Data.Single(x => x.Title == "Ginga Eiyuu Densetsu");
			logh.Type.Should().Be("OVA");
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeTopAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeTopAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeTopAsync_ValidTypeInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeTopAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeTopAsync_SecondPage_ShouldParseAnimeSecondPage()
		{
			// When
			var top = await _jikan.GetAnimeTopAsync(2);

			// Then
			var titles = top.Data.Select(x => x.Title);
			using (new AssertionScope())
			{
				titles.Should().Contain("Monster");
				titles.Should().Contain("Mushishi Zoku Shou");
			}
		}
	}
}