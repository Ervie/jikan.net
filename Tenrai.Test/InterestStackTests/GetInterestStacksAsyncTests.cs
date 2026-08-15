using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.InterestStackTests
{
	[Collection("TenraiTests")]
	public class GetInterestStacksAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetInterestStacksAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetInterestStacksAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetInterestStacksAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetInterestStacksAsync_NoParameter_ShouldParseFirstPageOfStacks()
		{
			// When
			var stacks = await _tenrai.GetInterestStacksAsync();

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().HaveCount(25);
				stacks.Pagination.CurrentPage.Should().Be(1);
				stacks.Pagination.HasNextPage.Should().BeTrue();
				stacks.Data.Should().OnlyContain(stack => !string.IsNullOrWhiteSpace(stack.Title));
				stacks.Data.Should().OnlyContain(stack => stack.StackType == "anime" || stack.StackType == "manga");
				stacks.Data.Should().OnlyContain(stack => stack.MalId > 0);
				stacks.Data.Should().OnlyContain(stack => stack.CreatedAt.HasValue);
			}
		}

		[Fact]
		public async Task GetInterestStacksAsync_SecondPage_ShouldReturnDifferentStacks()
		{
			// When
			var firstPage = await _tenrai.GetInterestStacksAsync();
			var secondPage = await _tenrai.GetInterestStacksAsync(2);

			// Then
			using (new AssertionScope())
			{
				secondPage.Pagination.CurrentPage.Should().Be(2);
				secondPage.Data.Should().HaveCount(25);
				secondPage.Data.Select(stack => stack.MalId).Should().NotIntersectWith(firstPage.Data.Select(stack => stack.MalId));
			}
		}
	}
}
