using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.InterestStackTests
{
	[Collection("TenraiTests")]
	public class GetEntityInterestStacksAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetEntityInterestStacksAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeInterestStacksAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeInterestStacksAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaInterestStacksAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMangaInterestStacksAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetAnimeInterestStacksAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetAnimeInterestStacksAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMangaInterestStacksAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMangaInterestStacksAsync(2, page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetAnimeInterestStacksAsync_BebopId_ShouldReturnOnlyAnimeStacks()
		{
			// When
			var stacks = await _tenrai.GetAnimeInterestStacksAsync(1);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().NotBeEmpty();
				stacks.Data.Should().OnlyContain(stack => stack.StackType == "anime");
				stacks.Data.Should().OnlyContain(stack => !string.IsNullOrWhiteSpace(stack.AuthorUsername));
				stacks.Pagination.Items.Total.Should().BeGreaterThan(0);
			}
		}

		[Fact]
		public async Task GetAnimeInterestStacksAsync_BebopIdSecondPage_ShouldReturnDifferentStacks()
		{
			// When
			var firstPage = await _tenrai.GetAnimeInterestStacksAsync(1);
			var secondPage = await _tenrai.GetAnimeInterestStacksAsync(1, 2);

			// Then
			using (new AssertionScope())
			{
				secondPage.Pagination.CurrentPage.Should().Be(2);
				secondPage.Data.Should().NotBeEmpty();
				secondPage.Data.Select(stack => stack.MalId).Should().NotIntersectWith(firstPage.Data.Select(stack => stack.MalId));
			}
		}

		[Fact]
		public async Task GetMangaInterestStacksAsync_BerserkId_ShouldReturnOnlyMangaStacks()
		{
			// When
			var stacks = await _tenrai.GetMangaInterestStacksAsync(2);

			// Then
			using (new AssertionScope())
			{
				stacks.Data.Should().NotBeEmpty();
				stacks.Data.Should().OnlyContain(stack => stack.StackType == "manga");
				stacks.Pagination.Items.Total.Should().BeGreaterThan(0);
			}
		}
	}
}
