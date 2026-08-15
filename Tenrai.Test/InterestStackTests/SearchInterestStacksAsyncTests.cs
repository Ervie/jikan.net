using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.InterestStackTests
{
	[Collection("TenraiTests")]
	public class SearchInterestStacksAsyncTests
	{
		private readonly ITenrai _tenrai;

		public SearchInterestStacksAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task SearchInterestStacksAsync_NullConfig_ShouldThrowValidationException()
		{
			// When
			var func = _tenrai.Awaiting(x => x.SearchInterestStacksAsync(null));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task SearchInterestStacksAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// Given
			var searchConfig = new InterestStackSearchConfig { Page = page };

			// When
			var func = _tenrai.Awaiting(x => x.SearchInterestStacksAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(26)]
		[InlineData(int.MaxValue)]
		public async Task SearchInterestStacksAsync_PageSizeOverCap_ShouldThrowValidationException(int pageSize)
		{
			// Given
			var searchConfig = new InterestStackSearchConfig { PageSize = pageSize };

			// When
			var func = _tenrai.Awaiting(x => x.SearchInterestStacksAsync(searchConfig));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(InterestStackType.Anime, "anime")]
		[InlineData(InterestStackType.Manga, "manga")]
		public async Task SearchInterestStacksAsync_GivenType_ShouldFilterByStackType(InterestStackType type, string expectedStackType)
		{
			// Given
			var searchConfig = new InterestStackSearchConfig { Type = type };

			// When
			var stacks = await _tenrai.SearchInterestStacksAsync(searchConfig);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().NotBeEmpty();
				stacks.Data.Should().OnlyContain(stack => stack.StackType == expectedStackType);
			}
		}

		[Fact]
		public async Task SearchInterestStacksAsync_PageSize_ShouldReturnRequestedNumberOfStacks()
		{
			// Given
			var searchConfig = new InterestStackSearchConfig { PageSize = 5 };

			// When
			var stacks = await _tenrai.SearchInterestStacksAsync(searchConfig);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().HaveCount(5);
				stacks.Pagination.Items.PerPage.Should().Be(5);
			}
		}

		[Fact]
		public async Task SearchInterestStacksAsync_OrderByRestackCountDescending_ShouldReturnMostRestackedFirst()
		{
			// Given
			var searchConfig = new InterestStackSearchConfig
			{
				OrderBy = InterestStackSearchOrderBy.RestackCount,
				SortDirection = SortDirection.Descending
			};

			// When
			var stacks = await _tenrai.SearchInterestStacksAsync(searchConfig);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().NotBeEmpty();
				stacks.Data.Select(stack => stack.RestackCount).Should().BeInDescendingOrder();
				stacks.Data.First().RestackCount.Should().BeGreaterThan(0);
			}
		}

		[Fact]
		public async Task SearchInterestStacksAsync_Query_ShouldNarrowResults()
		{
			// Given
			var searchConfig = new InterestStackSearchConfig { Query = "witch" };

			// When
			var unfiltered = await _tenrai.GetInterestStacksAsync();
			var stacks = await _tenrai.SearchInterestStacksAsync(searchConfig);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().NotBeEmpty();
				stacks.Pagination.Items.Total.Should().BeLessThan(unfiltered.Pagination.Items.Total);
			}
		}

		[Fact]
		public async Task SearchInterestStacksAsync_SfwDisabled_ShouldReturnMoreStacksThanSfw()
		{
			// Given
			var sfwConfig = new InterestStackSearchConfig();
			var everythingConfig = new InterestStackSearchConfig { Sfw = false };

			// When
			var sfw = await _tenrai.SearchInterestStacksAsync(sfwConfig);
			var everything = await _tenrai.SearchInterestStacksAsync(everythingConfig);

			// Then
			sfw.Pagination.Items.Total.Should().BeLessThan(everything.Pagination.Items.Total);
		}
	}
}
