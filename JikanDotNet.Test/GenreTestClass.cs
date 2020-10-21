using FluentAssertions;
using FluentAssertions.Execution;
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
			// When
			var genre = await _jikan.GetAnimeGenre(1);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(3300);
				genre.Metadata.Name.Should().Be("Action Anime");
				genre.Metadata.MalId.Should().Be(genre.MalId);
			}
		}

		[Fact]
		public async Task GetAnimeGenre_ActionGenreId_ShouldParseAnimeActionGenre()
		{
			// When
			var genre = await _jikan.GetAnimeGenre(1);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(3300);
				genre.Metadata.Name.Should().Be("Action Anime");
				genre.Anime.First().Title.Should().Be("Shingeki no Kyojin");
			}
		}

		[Fact]
		public async Task GetAnimeGenre_MechaGenreId_ShouldParseAnimeMechaGenre()
		{
			// When
			var genre = await _jikan.GetAnimeGenre(GenreSearch.Mecha);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(1000);
				genre.Metadata.Name.Should().Be("Mecha Anime");
				genre.Metadata.MalId.Should().Be(18);
				genre.Anime.First().Title.Should().Be("Code Geass: Hangyaku no Lelouch");
			}
		}

		[Fact]
		public async Task GetAnimeGenre_MysteryGenreIdSecondPage_ShouldParseAnimeMysteryGenreMetadata()
		{
			// When
			var genre = await _jikan.GetAnimeGenre(GenreSearch.Mystery, 2);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(600);
				genre.Metadata.Name.Should().Be("Mystery Anime");
				genre.Metadata.MalId.Should().Be(genre.MalId);
			}
		}

		[Fact]
		public async Task GetAnimeGenre_ActionGenreIdSecondPage_ShouldParseAnimeMysteryGenre()
		{
			// When
			var genre = await _jikan.GetAnimeGenre(GenreSearch.Mystery, 2);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(600);
				genre.Metadata.Name.Should().Be("Mystery Anime");
				genre.Anime.Should().HaveCount(100);
			}
		}

		[Fact]
		public async Task GetMangaGenre_ActionGenreId_ShouldParseMangaActionGenre()
		{
			// When
			var genre = await _jikan.GetMangaGenre(1);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(6000);
				genre.Metadata.Name.Should().Be("Action Manga");
				genre.Manga.First().Title.Should().Be("Shingeki no Kyojin");
			}
		}

		[Fact]
		public async Task GetMangaGenre_MechaGenreId_ShouldParseMangaMechaGenre()
		{
			// When
			var genre = await _jikan.GetMangaGenre(GenreSearch.Mecha);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(600);
				genre.Metadata.Name.Should().Be("Mecha Manga");
				genre.Metadata.MalId.Should().Be(18);
				genre.Manga.First().Title.Should().Be("Neon Genesis Evangelion");
			}
		}

		[Fact]
		public async Task GetMangaGenre_DramaGenreId_ShouldParseMangaDramaGenre()
		{
			// When
			var genre = await _jikan.GetMangaGenre(GenreSearch.Drama, 2);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(600);
				genre.Metadata.Name.Should().Be("Drama Manga");
				genre.Manga.Should().HaveCount(100);
			}
		}

	}
}