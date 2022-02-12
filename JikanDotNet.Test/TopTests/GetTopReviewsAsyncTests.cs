using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetTopReviewsAsyncTests
	{
		private readonly IJikan _jikan;

		public GetTopReviewsAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetTopReviewsAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetTopReviewsAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopReviewsAsync_NoParameter_ShouldParseTopReviews()
		{
			// When
			var reviews = await _jikan.GetTopReviewsAsync();

			// Then
			using var _ = new AssertionScope();
			reviews.Data.Count.Should().Be(50);
			reviews.Pagination.HasNextPage.Should().BeTrue();
		}

		[Fact]
		public async Task GetTopReviewsAsync_sECONDpAGE_ShouldParseTopReviewsSecondPage()
		{
			// When
			var reviews = await _jikan.GetTopReviewsAsync(2);

			// Then
			using var _ = new AssertionScope();
			reviews.Data.Count.Should().Be(50);
			reviews.Pagination.HasNextPage.Should().BeTrue();
		}
	}
}