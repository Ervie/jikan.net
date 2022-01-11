using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MangaTests
{
	public class GetMangaAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMangaAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaAsync_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaAsync(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetMangaAsync_CorrectId_ShouldReturnNotNullManga(long malId)
		{
			// When
			var returedManga = await _jikan.GetMangaAsync(malId);

			// Then
			returedManga.Should().NotBeNull();
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(5)]
		[InlineData(6)]
		public void GetMangaAsync_WrongId_ShouldReturnNullManga(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaAsync(malId));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetMangaAsync_BerserkId_ShouldParseBerserk()
		{
			// When
			var berserkManga = await _jikan.GetMangaAsync(2);

			// Then
			berserkManga.Data.Title.Should().Be("Berserk");
		}

		[Fact]
		public async Task GetMangaAsync_MonsterId_ShouldParseMonster()
		{
			// When
			var monsterManga = await _jikan.GetMangaAsync(1);

			// Then
			monsterManga.Data.Title.Should().Be("Monster");
		}

		[Fact]
		public async Task GetMangaAsync_YotsubatoId_ShouldParseYotsubatoInformation()
		{
			// When
			var yotsubatoManga = await _jikan.GetMangaAsync(104);

			// Then
			using (new AssertionScope())
			{
				yotsubatoManga.Data.Status.Should().Be("Publishing");
				yotsubatoManga.Data.Published.From.Value.Year.Should().Be(2003);
				yotsubatoManga.Data.Chapters.Should().BeNull();
				yotsubatoManga.Data.Volumes.Should().BeNull();
				yotsubatoManga.Data.Type.Should().Be("Manga");
			}
		}

		[Fact]
		public async Task GetMangaAsync_OnePieceId_ShouldParseOnePieceCollections()
		{
			// When
			var onePieceManga = await _jikan.GetMangaAsync(13);

			// Then
			using (new AssertionScope())
			{
				onePieceManga.Data.Authors.Should().ContainSingle();
				onePieceManga.Data.Serializations.Should().ContainSingle();
				onePieceManga.Data.Genres.Should().HaveCount(4);
				onePieceManga.Data.Authors.First().ToString().Should().Be("Oda, Eiichiro");
				onePieceManga.Data.Serializations.First().ToString().Should().Be("Shounen Jump (Weekly)");
				onePieceManga.Data.Genres.First().ToString().Should().Be("Action");
				onePieceManga.Data.Demographics.First().ToString().Should().Be("Shounen");
				onePieceManga.Data.Themes.First().ToString().Should().Be("Super Power");
			}
		}
	}
}