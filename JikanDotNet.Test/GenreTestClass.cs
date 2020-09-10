using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class GenreTestClass
	{
		private readonly IJikan _jikan;

		public GenreTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetAnimeGenre_ActionGenreId_ShouldParseAnimeActionGenreMetadata()
		{
			AnimeGenre genre = await _jikan.GetAnimeGenre(1);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 3300);
			Assert.Equal("Action Anime", genre.Metadata.Name);
			Assert.Equal(genre.Metadata.MalId, genre.MalId);
		}

		[Fact]
		public async Task GetAnimeGenre_ActionGenreId_ShouldParseAnimeActionGenre()
		{
			AnimeGenre genre = await _jikan.GetAnimeGenre(1);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 3300);
			Assert.Equal("Action Anime", genre.Metadata.Name);
			Assert.Equal("Shingeki no Kyojin", genre.Anime.First().Title);
		}

		[Fact]
		public async Task GetAnimeGenre_MechaGenreId_ShouldParseAnimeMechaGenre()
		{
			AnimeGenre genre = await _jikan.GetAnimeGenre(GenreSearch.Mecha);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 1000);
			Assert.Equal("Mecha Anime", genre.Metadata.Name);
			Assert.Equal(18, genre.Metadata.MalId);
			Assert.Equal("Code Geass: Hangyaku no Lelouch", genre.Anime.First().Title);
		}

		[Fact]
		public async Task GetAnimeGenre_MysteryGenreIdSecondPage_ShouldParseAnimeMysteryGenreMetadata()
		{
			AnimeGenre genre = await _jikan.GetAnimeGenre(GenreSearch.Mystery, 2);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Mystery Anime", genre.Metadata.Name);
			Assert.Equal(genre.MalId, genre.Metadata.MalId);
		}

		[Fact]
		public async Task GetAnimeGenre_ActionGenreIdSecondPage_ShouldParseAnimeMysteryGenre()
		{
			AnimeGenre genre = await _jikan.GetAnimeGenre(GenreSearch.Mystery, 2);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Mystery Anime", genre.Metadata.Name);
			Assert.Equal(100, genre.Anime.Count);
		}

		[Fact]
		public async Task GetMangaGenre_ActionGenreId_ShouldParseMangaActionGenre()
		{
			MangaGenre genre = await _jikan.GetMangaGenre(1);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 6000);
			Assert.Equal("Action Manga", genre.Metadata.Name);
			Assert.Equal("Shingeki no Kyojin", genre.Manga.First().Title);
		}

		[Fact]
		public async Task GetMangaGenre_MechaGenreId_ShouldParseMangaMechaGenre()
		{
			MangaGenre genre = await _jikan.GetMangaGenre(GenreSearch.Mecha);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Mecha Manga", genre.Metadata.Name);
			Assert.Equal(18, genre.Metadata.MalId);
			Assert.Equal("Neon Genesis Evangelion", genre.Manga.First().Title);
		}

		[Fact]
		public async Task GetMangaGenre_DramaGenreId_ShouldParseMangaDramaGenre()
		{
			MangaGenre genre = await _jikan.GetMangaGenre(GenreSearch.Drama, 2);

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Drama Manga", genre.Metadata.Name);
			Assert.Equal(100, genre.Manga.Count);
		}

	}
}