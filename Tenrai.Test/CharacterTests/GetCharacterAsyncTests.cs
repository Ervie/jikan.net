using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using FluentAssertions.Reflection;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	public async Task GetCharacterAsync_CorrectId_ShouldReturnNotNullCharacter(long malId)
	{
		// When
		var returnedCharacter = await _tenrai.GetCharacterAsync(malId);

		// Then
		returnedCharacter.Should().NotBeNull();
	}

	[Theory]
	[InlineData(8)]
	[InlineData(9)]
	[InlineData(10)]
	public void GetCharacterAsync_WrongId_ShouldReturnNullCharacter(long malId)
	{
		// When & Then
		Assert.ThrowsAnyAsync<TenraiRequestException>(() => _tenrai.GetCharacterAsync(malId));
	}

	[Fact]
	public async Task GetCharacterAsync_IchigoKurosakiId_ShouldParseIchigoKurosaki()
	{
		// When
		var ichigo = await _tenrai.GetCharacterAsync(5);

		// Then
		using var _ = new AssertionScope();
		ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
		ichigo.Data.NameKanji.Should().Be("黒崎 一護");
	}

	[Fact]
	public async Task GetCharacterAsync_IchigoKurosakiId_ShouldParseIchigoKurosakiAboutNotNull()
	{
		// When
		var ichigo = await _tenrai.GetCharacterAsync(5);

		// Then
		ichigo.Data.About.Should().NotBeNullOrEmpty();
	}

	[Fact]
	public async Task GetCharacterAsync_GetBlackId_ShouldParseJetBlackNicknames()
	{
		// When
		var jetBlack = await _tenrai.GetCharacterAsync(3);

		// Then
		jetBlack.Data.Nicknames.Should().HaveCount(2);
	}
}