using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.AnimeTests;

[Collection("TenraiTests")]
public class GetAnimeNewsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetAnimeNewsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeNews_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeNewsAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetAnimeNews_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeNewsAsync(1, page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetAnimeNews_BebopId_ShouldParseCowboyBebopNews()
	{
		// When
		var bebop = await _tenrai.GetAnimeNewsAsync(1);

		// Then
		using (new AssertionScope())
		{
			bebop.Data.Should().HaveCountGreaterOrEqualTo(5);
			bebop.Data.Select(x => x.Author).Should().Contain("Snow");
			bebop.Pagination.Items.Should().BeNull();
			bebop.Pagination.CurrentPage.Should().BeNull();
			bebop.Pagination.LastVisiblePage.Should().BePositive();
		}
	}

	[Fact]
	public async Task GetAnimeNews_BebopIdWithPage_ShouldParseCowboyBebopNews()
	{
		// When
		var bebop = await _tenrai.GetAnimeNewsAsync(1, 1);

		// Then
		using (new AssertionScope())
		{
			bebop.Data.Should().HaveCountGreaterOrEqualTo(5);
			bebop.Data.Select(x => x.Author).Should().Contain("Snow");
		}
	}

	[Fact]
	public async Task GetAnimeNews_BebopIdWithNextPage_ShouldParseZeroNews()
	{
		// When
		var bebop = await _tenrai.GetAnimeNewsAsync(1, 2);

		// Then
		bebop.Data.Should().BeEmpty();
	}
}