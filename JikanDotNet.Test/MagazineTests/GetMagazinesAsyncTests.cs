using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.MagazineTests
{
	public class GetMagazinesAsyncTests
	{
		private readonly IJikan _jikan;

		public GetMagazinesAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetMagazinesAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMagazinesAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetMagazinesAsync_NoParameter_ShouldParsePaginationData()
		{
			// When
			var results = await _jikan.GetMagazinesAsync();

			// Then
			using var _ = new AssertionScope();
			results.Data.Should().HaveCount(25);
			results.Pagination.HasNextPage.Should().BeTrue();
			results.Pagination.LastVisiblePage.Should().Be(47);
		}

		[Fact]
		public async Task GetMagazinesAsync_NoParameter_ShouldParseMagazines()
		{
			// When
			var results = await _jikan.GetMagazinesAsync();

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
			var results = await _jikan.GetMagazinesAsync(2);

			// Then
			var names = results.Data.Select(x => x.Name);
			using var _ = new AssertionScope();
			names.Should().Contain("GFantasy");
			names.Should().Contain("Shounen Magazine (Monthly)");
			names.Should().Contain("Betsucomi");
		}
	}
}