using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeThemesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeThemesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeThemesAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeThemesAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeThemesAsync_BebopId_ShouldParseCowboyBebopOpeningsAndEndings()
		{
			// When
			var result = await _jikan.GetAnimeThemesAsync(1);

			// Then
			using var _ = new AssertionScope();
			result.Data.Openings.Should().ContainSingle().Which.Equals("\"Tank!\" by The Seatbelts (eps 1-25)");
			result.Data.Endings.Should().HaveCount(3);
		}
	}
}