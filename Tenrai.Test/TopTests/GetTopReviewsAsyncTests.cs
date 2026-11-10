using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.TopTests;

[Collection("TenraiTests")]
public class GetTopReviewsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetTopReviewsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetTopReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetTopReviewsAsync(new TopReviewsConfig { Page = page }));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetTopReviewsAsync_NoParameter_ShouldParseTopReviews()
	{
		// When
		var reviews = await _tenrai.GetTopReviewsAsync();

		// Then
		using var _ = new AssertionScope();
		reviews.Data.Count.Should().Be(25);
		reviews.Pagination.HasNextPage.Should().BeTrue();
	}

	[Fact]
	public async Task GetTopReviewsAsync_sECONDpAGE_ShouldParseTopReviewsSecondPage()
	{
		// When
		var reviews = await _tenrai.GetTopReviewsAsync(new TopReviewsConfig { Page = 2 });

		// Then
		using var _ = new AssertionScope();
		reviews.Data.Count.Should().Be(25);
		reviews.Pagination.HasNextPage.Should().BeTrue();
	}
}