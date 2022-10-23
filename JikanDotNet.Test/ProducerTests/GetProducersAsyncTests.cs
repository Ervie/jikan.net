using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ProducerTests
{
	public class GetProducersAsyncTests
	{
		private readonly IJikan _jikan;

		public GetProducersAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetProducersAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetProducersAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetProducersAsync_NoParameter_ShouldParsePaginationData()
		{
			// When
			var results = await _jikan.GetProducersAsync();

			// Then
			using var _ = new AssertionScope();
			results.Data.Should().HaveCount(25);
			results.Pagination.HasNextPage.Should().BeTrue();
			results.Pagination.LastVisiblePage.Should().BeGreaterThan(50);
		}

		[Fact]
		public async Task GetProducersAsync_NoParameter_ShouldParseProducers()
		{
			// When
			var results = await _jikan.GetProducersAsync();

			// Then
			var names = results.Data.SelectMany(x => x.Titles).Select(y => y.Title);
			using var _ = new AssertionScope();
			names.Should().Contain("Pierrot");
			names.Should().Contain("Kyoto Animation");
			names.Should().Contain("Gonzo");
		}

		[Fact]
		public async Task GetProducersAsync_SecondPage_ShouldParseProducers()
		{
			// When
			var results = await _jikan.GetProducersAsync(2);

			// Then
			var names = results.Data.SelectMany(x => x.Titles).Select(y => y.Title);
			using var _ = new AssertionScope();
			names.Should().Contain("Manglobe");
			names.Should().Contain("Studio Deen");
			names.Should().Contain("Satelight");
		}
	}
}