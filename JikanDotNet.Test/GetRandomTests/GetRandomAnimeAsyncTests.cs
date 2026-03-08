using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	[Collection("JikanTests")]
	public class GetRandomAnimeAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomAnimeAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
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