using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.CharacterTests;

[Collection("TenraiTests")]
public class GetCharacterPicturesAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetCharacterPicturesAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetCharacterPicturesAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetCharacterPicturesAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetCharacterPicturesAsync_SpikeSpiegelId_ShouldParseSpikeSpiegelImages()
	{
		// When
		var spike = await _tenrai.GetCharacterPicturesAsync(1);

		// Then
		using var _ = new AssertionScope();
		spike.Data.Should().HaveCountGreaterOrEqualTo(15);
		spike.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
	}

	[Fact]
	public async Task GetCharacterPicturesAsync_SharoId_ShouldParseKirimaSharoImages()
	{
		// When
		var kirimaSharo = await _tenrai.GetCharacterPicturesAsync(94947);

		// Then
		using var _ = new AssertionScope();
		kirimaSharo.Data.Should().HaveCount(9);
		kirimaSharo.Data.Should().OnlyContain(x => !string.IsNullOrWhiteSpace(x.JPG.ImageUrl));
	}
}