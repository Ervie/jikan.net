using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.AnimeTests
{
	public class GetAnimeMoreInfoAsyncTests
	{
		private readonly IJikan _jikan;

		public GetAnimeMoreInfoAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeMoreInfoAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeMoreInfoAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetAnimeMoreInfoAsync_BebopId_ShouldParseCowboyBebopMoreInfo()
		{
			// When
			var bebop = await _jikan.GetAnimeMoreInfoAsync(1);

			// Then
			bebop.Data.Info.Should().StartWith("Suggested Order of Viewing");
		}
	}
}