using System.Linq;
using FluentAssertions;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterFullDataAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterFullDataAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterFullDataAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterFullDataAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetCharacterFullDataAsync_IchigoKurosakiId_ShouldParseIchigoKurosaki()
	{
		// When
		var ichigo = await _tenrai.GetCharacterFullDataAsync(5);

		// Then
		using var _ = new AssertionScope();
		ichigo.Data.Name.Should().Be("Ichigo Kurosaki");
		ichigo.Data.NameKanji.Should().Be("黒崎 一護");
		ichigo.Data.Animeography.Should().HaveCountGreaterOrEqualTo(10);
		ichigo.Data.Animeography.Select(x => x.Anime.Title).Should().Contain("Bleach");
		ichigo.Data.Mangaography.Should().HaveCountGreaterOrEqualTo(5);
		ichigo.Data.VoiceActors.Should().HaveCountGreaterOrEqualTo(10);
		ichigo.Data.VoiceActors.Should().Contain(x => x.Person.Name.Equals("Morita, Masakazu"));
	}
}