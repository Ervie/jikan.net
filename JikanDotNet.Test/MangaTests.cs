using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MangaTests
	{
		private readonly IJikan _jikan;

		public MangaTests()
		{
			_jikan = new Jikan(true);
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetManga_InvalidId_ShouldThrowValidationException(long id)
		{
			// When
			Func<Task<Manga>> func = _jikan.Awaiting(x => x.GetManga(id));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public async Task GetManga_CorrectId_ShouldReturnNotNullManga(long malId)
		{
			// When
			var returedManga = await _jikan.GetManga(malId);

			// Then
			returedManga.Should().NotBeNull();
		}

		[Theory]
		[InlineData(-1)]
		[InlineData(5)]
		[InlineData(6)]
		public void GetManga_WrongId_ShouldReturnNullMana(long malId)
		{
			// When
			Func<Task<Manga>> func = _jikan.Awaiting(x => x.GetManga(malId));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public async Task GetManga_BerserkId_ShouldParseBerserk()
		{
			// When
			var berserkManga = await _jikan.GetManga(2);

			// Then
			berserkManga.Title.Should().Be("Berserk");
		}

		[Fact]
		public async Task GetManga_MonsterId_ShouldParseMonster()
		{
			// When
			var monsterManga = await _jikan.GetManga(1);

			// Then
			monsterManga.Title.Should().Be("Monster");
		}

		[Fact]
		public async Task GetManga_MonsterId_ShouldParseMonsterRelated()
		{
			// When
			var monsterManga = await _jikan.GetManga(1);

			// Then
			using (new AssertionScope())
			{
				monsterManga.Related.Adaptations.Should().ContainSingle();
				monsterManga.Related.SideStories.Should().ContainSingle();
			}
		}

		[Fact]
		public async Task GetManga_YotsubatoId_ShouldParseYotsubatoInformation()
		{
			// When
			var yotsubatoManga = await _jikan.GetManga(104);

			// Then
			using (new AssertionScope())
			{
				yotsubatoManga.Status.Should().Be("Publishing");
				yotsubatoManga.Published.From.Value.Year.Should().Be(2003);
				yotsubatoManga.Chapters.Should().BeNull();
				yotsubatoManga.Volumes.Should().BeNull();
				yotsubatoManga.Type.Should().Be("Manga");
			}
		}

		[Fact]
		public async Task GetManga_OnePieceId_ShouldParseOnePieceCollections()
		{
			// When
			var onePieceManga = await _jikan.GetManga(13);

			// Then
			using (new AssertionScope())
			{
				onePieceManga.Authors.Should().ContainSingle();
				onePieceManga.Serializations.Should().ContainSingle();
				onePieceManga.Genres.Should().HaveCount(6);
				onePieceManga.Authors.First().ToString().Should().Be("Oda, Eiichiro");
				onePieceManga.Serializations.First().ToString().Should().Be("Shounen Jump (Weekly)");
				onePieceManga.Genres.First().ToString().Should().Be("Action");
			}
		}

		[Fact]
		public async Task GetManga_MetallicaMetallucaId_ShouldParseMangaWithNoRelatedAdaptations()
		{
			// When
			var returnedManga = await _jikan.GetManga(19983);

			// Then
			returnedManga.Related.Adaptations.Should().BeNull();
		}
	}
}