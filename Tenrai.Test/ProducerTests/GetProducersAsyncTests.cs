using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.ProducerTests;

[Collection("TenraiTests")]
public class GetProducersAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetProducersAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetProducersAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetProducersAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetProducersAsync_NoParameter_ShouldParsePaginationData()
	{
		// When
		var results = await _tenrai.GetProducersAsync();

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
		var results = await _tenrai.GetProducersAsync();

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
		var results = await _tenrai.GetProducersAsync(2);

		// Then
		var names = results.Data.SelectMany(x => x.Titles).Select(y => y.Title);
		using var _ = new AssertionScope();
		names.Should().Contain("Manglobe");
		names.Should().Contain("Studio Deen");
		names.Should().Contain("Satelight");
	}

	[Fact]
	public async Task GetProducersAsync_QueryBones_FindsBones()
	{
		var results = await _tenrai.SearchProducersAsync(new ProducerSearchConfig { Query = "bones" });

		results.Data.SelectMany(x => x.Titles).Select(y => y.Title).Should().Contain("Bones");
	}
}