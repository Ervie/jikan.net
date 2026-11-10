using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MagazineTests
{
	[Collection("TenraiTests")]
	public class GetMagazinesAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetMagazinesAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMagazinesAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _tenrai.Awaiting(x => x.GetMagazinesAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<TenraiValidationException>();
		}

		[Fact]
		public async Task GetMagazinesAsync_NoParameter_ShouldParsePaginationData()
		{
			// When
			var results = await _tenrai.GetMagazinesAsync();

			// Then
			using var _ = new AssertionScope();
			results.Data.Should().HaveCount(25);
			results.Pagination.HasNextPage.Should().BeTrue();
			results.Pagination.LastVisiblePage.Should().BeGreaterOrEqualTo(49);
		}

		[Fact]
		public async Task GetMagazinesAsync_NoParameter_ShouldParseMagazines()
		{
			// When
			var results = await _tenrai.GetMagazinesAsync();

			// Then
			var names = results.Data.Select(x => x.Name);
			using var _ = new AssertionScope();
			names.Should().Contain("Big Comic Original");
			names.Should().Contain("Young Animal");
			names.Should().Contain("Young Magazine (Monthly)");
		}

		[Fact]
		public async Task GetMagazinesAsync_SecondPage_ShouldParseMagazines()
		{
			// When
			var results = await _tenrai.GetMagazinesAsync(2);

			// Then
			var names = results.Data.Select(x => x.Name);
			using var _ = new AssertionScope();
			names.Should().Contain("GFantasy");
			names.Should().Contain("Shounen Magazine (Monthly)");
			names.Should().Contain("Betsucomi");
		}

		[Fact]
		public async Task GetMagazinesAsync_JumpByCount_HasShounenJumpPlus()
		{
			var config = new MagazineSearchConfig { Query = "jump", OrderBy = MagazineSearchOrderBy.Count, SortDirection = SortDirection.Descending };

			var results = await _tenrai.SearchMagazinesAsync(config);

			using var _ = new AssertionScope();
			results.Data.Select(x => x.TotalCount).Should().BeInDescendingOrder();
			results.Data.Should().Contain(x => x.Name == "Shounen Jump+");
		}
	}
}