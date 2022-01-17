using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetTopCharactersAsyncTests
	{
		private readonly IJikan _jikan;

		public GetTopCharactersAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetTopCharactersAsync_NoParameters_ShouldParseLelouchLamperouge()
		{
			// When
			var top = await _jikan.GetTopCharactersAsync();

			// Then
			var lelouch = top.Data.First();
			using (new AssertionScope())
			{
				lelouch.Name.Should().Be("Lelouch Lamperouge");
				lelouch.Nicknames.Should().HaveCount(5);
				lelouch.MalId.Should().Be(417);
				lelouch.Favorites.Should().BeGreaterThan(85000);
			}
		}

		[Fact]
		public async Task GetTopCharactersAsync_NoParameters_ShouldParseLLawliet()
		{
			// When
			var top = await _jikan.GetTopCharactersAsync();

			// Then
			var l = top.Data.Skip(2).First();
			using (new AssertionScope())
			{
				l.Name.Should().Be("L Lawliet");
				l.Nicknames.Should().HaveCount(4);
				l.MalId.Should().Be(71);
				l.Favorites.Should().BeGreaterThan(65000);
			}
		}

		[Fact]
		public async Task GetTopCharactersAsync_NoParameters_ShouldParseLuffyAnimeography()
		{
			// When
			var top = await _jikan.GetTopCharactersAsync();

			// Then
			using (new AssertionScope())
			{
				top.Data.Skip(3).First().Name.Should().Be("Luffy Monkey D.");
				top.Data.Skip(3).First().About.Should().StartWith("Name: Monkey D. Luffy");
			}
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetTopCharactersAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopCharactersAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopCharactersAsync_FifthPage_ShouldFindTachibanaKanade()
		{
			// When
			var top = await _jikan.GetTopCharactersAsync(5);

			// Then
			top.Data.Select(x => x.Name).Should().Contain("Kanade Tachibana");
		}
	}
}