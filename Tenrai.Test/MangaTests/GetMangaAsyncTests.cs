using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	public async Task GetMangaAsync_CorrectId_ShouldReturnNotNullManga(long malId)
	{
		// When
		var returedManga = await _tenrai.GetMangaAsync(malId);

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
		var func = _tenrai.Awaiting(x => x.GetMangaAsync(malId));

		// Then
		func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public async Task GetMangaAsync_BerserkId_ShouldParseBerserk()
	{
		// When
		var berserkManga = await _tenrai.GetMangaAsync(2);

		// Then
		berserkManga.Data.Title.Should().Be("Berserk");
	}

	[Fact]
	public async Task GetMangaAsync_MonsterId_ShouldParseMonster()
	{
		// When
		var monsterManga = await _tenrai.GetMangaAsync(1);

		// Then
		monsterManga.Data.Title.Should().Be("Monster");
	}

	[Fact]
	public async Task GetMangaAsync_YotsubatoId_ShouldParseYotsubatoInformation()
	{
		// When
		var yotsubatoManga = await _tenrai.GetMangaAsync(104);

		// Then
		using (new AssertionScope())
		{
			yotsubatoManga.Data.Status.Should().Be("Publishing");
			yotsubatoManga.Data.Published.From.Value.Year.Should().Be(2003);
			yotsubatoManga.Data.Chapters.Should().BeNull();
			yotsubatoManga.Data.Volumes.Should().BeNull();
			yotsubatoManga.Data.Type.Should().Be("Manga");
			yotsubatoManga.Data.Approved.Should().BeTrue();
		}
	}


	[Fact]
	public async Task GetMangaAsync_YotsubatoId_ShouldParseYotsubatoTitles()
	{
		// When
		var yotsubatoManga = await _tenrai.GetMangaAsync(104);

		// Then
		using var _ = new AssertionScope();
		yotsubatoManga.Data.Titles.Should().HaveCountGreaterOrEqualTo(5);
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Default") && x.Title.Equals("Yotsuba to!"));
		yotsubatoManga.Data.Titles.Should().Contain(x => x.Type.Equals("Synonym") && x.Title.Equals("Yotsuba and!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Japanese") && x.Title.Equals("よつばと!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("English") && x.Title.Equals("Yotsuba&!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("German") && x.Title.Equals("Yotsuba&!"));
		yotsubatoManga.Data.Titles.Should().ContainSingle(x => x.Type.Equals("Spanish") && x.Title.Equals("¡Yotsuba!"));
	}

	[Fact]
	public async Task GetMangaAsync_OnePieceId_ShouldParseOnePieceCollections()
	{
		// When
		var onePieceManga = await _tenrai.GetMangaAsync(13);

		// Then
		using (new AssertionScope())
		{
			onePieceManga.Data.Authors.Should().ContainSingle();
			onePieceManga.Data.Serializations.Should().ContainSingle();
			onePieceManga.Data.Genres.Should().HaveCount(3);
			onePieceManga.Data.Authors.First().ToString().Should().Be("Oda, Eiichiro");
			onePieceManga.Data.Serializations.First().ToString().Should().Be("Shounen Jump (Weekly)");
			onePieceManga.Data.Genres.First().ToString().Should().Be("Action");
			onePieceManga.Data.Demographics.First().ToString().Should().Be("Shounen");
			onePieceManga.Data.Themes.Should().BeEmpty();
		}
	}

	[Fact]
	public async Task GetMangaAsync_Berserk_AuthorsHaveRoles()
	{
		// When
		var berserk = await _tenrai.GetMangaAsync(2);

		// Then
		berserk.Data.Authors.Should().Contain(a => a.Name == "Miura, Kentarou" && a.Role == "Story & Art");
	}
}