using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeNewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeNewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeNews_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeNewsAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeNews_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeNewsAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeNews_BebopId_ShouldParseCowboyBebopNews()
		{
			// When
			var bebop = await _jikan.GetAnimeNewsAsync(1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(7);
				bebop.Data.Select(x => x.Author).Should().Contain("Snow");
			}
		}

		[Fact]
		public async Task GetAnimeNews_BebopIdWithPage_ShouldParseCowboyBebopNews()
		{
			// When
			var bebop = await _jikan.GetAnimeNewsAsync(1, 1);

			// Then
			using (new AssertionScope())
			{
				bebop.Data.Should().HaveCount(7);
				bebop.Data.Select(x => x.Author).Should().Contain("Snow");
			}
		}

		[Fact]
		public async Task GetAnimeNews_BebopIdWithNextPage_ShouldParseZeroNews()
		{
			// When
			var bebop = await _jikan.GetAnimeNewsAsync(1, 2);

			// Then
			bebop.Data.Should().BeEmpty();
		}
	}
}