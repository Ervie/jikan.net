using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class GenreTests
	{
		private readonly IJikan _jikan;

		public GenreTests()
		{
			_jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeGenre_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<AnimeGenre>> func = _jikan.Awaiting(x => x.GetAnimeGenre(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData((GenreSearch) int.MaxValue)]
		[InlineData((GenreSearch) int.MinValue)]
		public async Task GetAnimeGenre_InvalidGenre_ShouldThrowValidationException(GenreSearch genreSearch)
		{
			// When
			Func<Task<AnimeGenre>> func = this._jikan.Awaiting(x=> x.GetAnimeGenre(genreSearch));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeGenre_SecondPageInvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<AnimeGenre>> func = _jikan.Awaiting(x => x.GetAnimeGenre(id, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeGenre_ValidIdInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeGenre>> func = _jikan.Awaiting(x => x.GetAnimeGenre(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeGenre_ValidGenreInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<AnimeGenre>> func = _jikan.Awaiting(x => x.GetAnimeGenre(GenreSearch.Mystery, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((GenreSearch)int.MinValue)]
		[InlineData((GenreSearch) int.MaxValue)]
		public async Task GetAnimeGenre_InvalidGenreValidPage_ShouldThrowValidationException(GenreSearch genreSearch)
		{
			// When
			Func<Task<AnimeGenre>> func = _jikan.Awaiting(x => x.
				GetAnimeGenre(genreSearch, 1));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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
			var genre = await _jikan.GetAnimeGenre(GenreSearch.Action, 2);

			// Then
			using (new AssertionScope())
			{
				genre.Should().NotBeNull();
				genre.TotalCount.Should().BeGreaterThan(600);
				genre.Metadata.Name.Should().Be("Action Anime");
				genre.Anime.Should().HaveCount(100);
			}
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaGenre_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaGenre>> func = _jikan.Awaiting(x => x.GetMangaGenre(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((GenreSearch) int.MaxValue)]
		[InlineData((GenreSearch) int.MinValue)]
		public async Task GetMangaGenre_InvalidGenre_ShouldThrowValidationException(GenreSearch genreSearch)
		{
			// When
			Func<Task<MangaGenre>> func = this._jikan.Awaiting(x=> x.GetMangaGenre(genreSearch));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaGenre_SecondPageInvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<MangaGenre>> func = _jikan.Awaiting(x => x.GetMangaGenre(id, 2));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaGenre_ValidIdInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<MangaGenre>> func = _jikan.Awaiting(x => x.GetMangaGenre(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaGenre_ValidGenreInvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			Func<Task<MangaGenre>> func = _jikan.Awaiting(x => x.GetMangaGenre(GenreSearch.Mystery, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData((GenreSearch)int.MinValue)]
		[InlineData((GenreSearch) int.MaxValue)]
		public async Task GetMangaGenre_InvalidGenreValidPage_ShouldThrowValidationException(GenreSearch genreSearch)
		{
			// When
			Func<Task<MangaGenre>> func = _jikan.Awaiting(x => x.
				GetMangaGenre(genreSearch, 1));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
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