using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	public class GetRandomMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomMangaAsyncTests()
		{
			_jikan = new Jikan();
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