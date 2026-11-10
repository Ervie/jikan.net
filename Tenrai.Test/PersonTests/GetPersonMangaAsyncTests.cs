using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.PersonTests;

[Collection("TenraiTests")]
public class GetPersonMangaAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetPersonMangaAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetPersonMangaAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonMangaAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetPersonMangaAsync_YuasaId_ShouldParseMasaakiYuasaAnime()
	{
		// Given
		var yuasa = await _tenrai.GetPersonMangaAsync(5068);

		// Then
		yuasa.Data.Should().NotBeEmpty();
	}

	[Fact]
	public async Task GetPersonMangaAsync_EiichiroOdaId_ShouldParseEiichiroOda()
	{
		// Given
		var oda = await _tenrai.GetPersonMangaAsync(1881);

		// Then
		using (new AssertionScope())
		{
			oda.Data.Should().HaveCountGreaterOrEqualTo(15);
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece") && x.Position.Equals("Story & Art"));
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("Cross Epoch") && x.Position.Equals("Story & Art"));
			oda.Data.Should().Contain(x => x.Manga.Title.Equals("One Piece Novel: Mugiwara Stories") && x.Position.Equals("Art"));
		}
	}
}
