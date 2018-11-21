using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class GenreTestClass
	{
		private readonly IJikan jikan;

		public GenreTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseAnimeActionGenre()
		{
			AnimeGenre genre = Task.Run(() => jikan.GetAnimeGenre(1)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 3300);
			Assert.Equal("Action Anime", genre.Metadata.Name);
			Assert.Equal("Shingeki no Kyojin", genre.Anime.First().Title);
		}

		[Fact]
		public void ShouldParseAnimeMechaGenre()
		{
			AnimeGenre genre = Task.Run(() => jikan.GetAnimeGenre(GenreSearch.Mecha)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 1000);
			Assert.Equal("Mecha Anime", genre.Metadata.Name);
			Assert.Equal(18, genre.Metadata.MalId);
			Assert.Equal("Code Geass: Hangyaku no Lelouch", genre.Anime.First().Title);
		}

		[Fact]
		public void ShouldParseAnimeMysteryGenre()
		{
			AnimeGenre genre = Task.Run(() => jikan.GetAnimeGenre(GenreSearch.Mystery, 2)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Mystery Anime", genre.Metadata.Name);
			Assert.Equal(100, genre.Anime.Count);
		}

		[Fact]
		public void ShouldParseMangaActionGenre()
		{
			MangaGenre genre = Task.Run(() => jikan.GetMangaGenre(1)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 6000);
			Assert.Equal("Action Manga", genre.Metadata.Name);
			Assert.Equal("Naruto", genre.Manga.First().Title);
		}

		[Fact]
		public void ShouldParseMangaMechaGenre()
		{
			MangaGenre genre = Task.Run(() => jikan.GetMangaGenre(GenreSearch.Mecha)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Mecha Manga", genre.Metadata.Name);
			Assert.Equal(18, genre.Metadata.MalId);
			Assert.Equal("Neon Genesis Evangelion", genre.Manga.First().Title);
		}

		[Fact]
		public void ShouldParseMangaDramaGenre()
		{
			MangaGenre genre = Task.Run(() => jikan.GetMangaGenre(GenreSearch.Drama, 2)).Result;

			Assert.NotNull(genre);
			Assert.True(genre.TotalCount > 600);
			Assert.Equal("Drama Manga", genre.Metadata.Name);
			Assert.Equal(100, genre.Manga.Count);
		}

	}
}