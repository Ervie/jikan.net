using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.PersonTests;

[Collection("TenraiTests")]
public class GetPersonPicturesAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetPersonPicturesAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetPersonPicturesAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonPicturesAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetPersonPicturesAsync_WakamotoId_ShouldParseNorioWakamotoImages()
	{
		// Given
		var norioWakamoto = await _tenrai.GetPersonPicturesAsync(84);

		// Then
		norioWakamoto.Data.Should().HaveCountGreaterOrEqualTo(4);
	}

	[Fact]
	public async Task GetPersonPicturesAsync_SugitaId_ShouldParseSugitaTomokazuImages()
	{
		// Given
		var norioWakamoto = await _tenrai.GetPersonPicturesAsync(2);

		// Then
		norioWakamoto.Data.Should().HaveCountGreaterOrEqualTo(8);
	}
}
