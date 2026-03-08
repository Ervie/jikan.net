using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	[Collection("JikanTests")]
	public class GetRandomMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomMangaAsyncTests(JikanFixture jikanFixture)
		{
			_jikan = jikanFixture.Jikan;
		}

		[Fact]
		public async Task GetRandomMangaAsync_ShouldReturnNotNullManga()
		{
			// When
			var manga = await _jikan.GetRandomMangaAsync();

			// Then
			manga.Data.Should().NotBeNull();
		}
	}
}