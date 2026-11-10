using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterVoiceActorsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterVoiceActorsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterVoiceActorsAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterVoiceActorsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetCharacterVoiceActorsAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelVoiceActors()
	{
		// When
		var spike = await _tenrai.GetCharacterVoiceActorsAsync(1);

		// Then
		using var _ = new AssertionScope();
		spike.Data.Should().HaveCountGreaterOrEqualTo(13);
		spike.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Yamadera, Kouichi"));
		spike.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Blum, Steven"));
		spike.Data.Should().Contain(x => x.Language.Equals("German") && x.Person.Name.Equals("Neumann, Viktor"));
	}

	[Fact]
	public async Task GetCharacterVoiceActorsAsync_FayeValentinelId_ShouldParseFayeValentineVoiceActors()
	{
		// When
		var faye = await _tenrai.GetCharacterVoiceActorsAsync(2);

		// Then
		using (new AssertionScope())
		{
			faye.Data.Should().HaveCountGreaterOrEqualTo(12);
			faye.Data.Should().Contain(x => x.Language.Equals("Japanese") && x.Person.Name.Equals("Hayashibara, Megumi"));
			faye.Data.Should().Contain(x => x.Language.Equals("English") && x.Person.Name.Equals("Lee, Wendee"));
		}
	}
}
