using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.TopTests;

[Collection("TenraiTests")]
public class GetTopCharactersAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetTopCharactersAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetTopCharactersAsync_NoParameters_ShouldParseLelouchLamperouge()
	{
		// When
		var top = await _tenrai.GetTopCharactersAsync();

		// Then
		var lelouch = top.Data.First();
		using (new AssertionScope())
		{
			lelouch.Name.Should().Be("Lelouch Lamperouge");
			lelouch.Nicknames.Should().HaveCountGreaterOrEqualTo(5);
			lelouch.MalId.Should().Be(417);
			lelouch.Favorites.Should().BeGreaterThan(85000);
		}
	}

	[Fact]
	public async Task GetTopCharactersAsync_NoParameters_ShouldParseLLawliet()
	{
		// When
		var top = await _tenrai.GetTopCharactersAsync();

		// Then
		var l = top.Data.Skip(2).First();
		using (new AssertionScope())
		{
			l.Name.Should().Be("L Lawliet");
			l.Nicknames.Should().HaveCountGreaterOrEqualTo(4);
			l.MalId.Should().Be(71);
			l.Favorites.Should().BeGreaterThan(65000);
		}
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetTopCharactersAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetTopCharactersAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetTopCharactersAsync_FifthPage_ShouldFindMakima()
	{
		// When
		var top = await _tenrai.GetTopCharactersAsync(5);

		// Then
		top.Data.Select(x => x.Name).Should().Contain("Makima");
	}
}