using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	public class GetRandomAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomAnimeAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetRandomAnimeAsync_ShouldReturnNotNullAnime()
		{
			// When
			var anime = await _jikan.GetRandomAnimeAsync();

			// Then
			anime.Data.Should().NotBeNull();
		}
	}
}