using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.ProducerTests;

[Collection("TenraiTests")]
public class GetProducerFullDataAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetProducerFullDataAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetProducerFullDataAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetProducerFullDataAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetProducerFullDataAsync_PierrotId_ShouldParsePierrot()
	{
		// When
		var results = await _tenrai.GetProducerFullDataAsync(1);

		// Then
		using var _ = new AssertionScope();
		results.Data.Titles.Should().Contain(x => x.Title.Equals("Pierrot"));
		results.Data.TotalCount.Should().BeGreaterThan(250);
		results.Data.Established.Should().HaveYear(1979);
		results.Data.External.Should().HaveCountGreaterOrEqualTo(5);
		results.Data.External.Should().Contain(x => x.Name.Equals("pierrot.jp") && x.Url.Equals("http://pierrot.jp/"));
	}

	[Fact]
	public async Task GetProducerFullDataAsync_KyoAniId_ShouldParsePierrot()
	{
		// When
		var results = await _tenrai.GetProducerFullDataAsync(2);

		// Then
		using var _ = new AssertionScope();
		results.Data.Titles.Should().Contain(x => x.Title.Equals("Kyoto Animation"));
		results.Data.TotalCount.Should().BeGreaterThan(120);
		results.Data.Established.Should().HaveYear(1985);
		results.Data.About.Should().NotBeNullOrEmpty();
	}
}