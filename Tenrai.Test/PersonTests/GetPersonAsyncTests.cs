using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.PersonTests;

[Collection("TenraiTests")]
public class GetPersonAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetPersonAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetPersonAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	public async Task GetPersonAsync_CorrectId_ShouldReturnNotNullPerson(long malId)
	{
		// Given
		var returnedPerson = await _tenrai.GetPersonAsync(malId);

		// Then
		returnedPerson.Should().NotBeNull();
	}

	[Theory]
	[InlineData(13308)]
	[InlineData(13310)]
	[InlineData(13312)]
	public void GetPersonAsync_WrongId_ShouldReturnNullPerson(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonAsync(malId));

		// Then
		func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public async Task GetPersonAsync_WakamotoId_ShouldParseNorioWakamoto()
	{
		// Given
		var norioWakamoto = await _tenrai.GetPersonAsync(84);

		// Then
		using (new AssertionScope())
		{
			norioWakamoto.Data.Name.Should().Be("Norio Wakamoto");
			norioWakamoto.Data.Birthday.Value.Year.Should().Be(1945);
		}
	}
}
