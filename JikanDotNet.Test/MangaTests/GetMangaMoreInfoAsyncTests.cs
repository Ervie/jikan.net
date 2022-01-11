using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaMoreInfoAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaMoreInfoAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaMoreInfoAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaMoreInfoAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMangaMoreInfoAsync_BerserkId_ShouldParseBerserkMoreInfo()
		{
			// When
			var berserk = await _jikan.GetMangaMoreInfoAsync(2);

			// Then
			berserk.Data.Info.Should().Contain("The Prototype (1988)");
		}
	}
}