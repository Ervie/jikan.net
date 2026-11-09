using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.ProducerTests;

[Collection("TenraiTests")]
public class GetProducerAsyncTests
{
    private readonly ITenrai _tenrai;

    public GetProducerAsyncTests(TenraiFixture tenraiFixture)
    {
        _tenrai = tenraiFixture.TenraiClient;
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetProducerAsync_InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = _tenrai.Awaiting(x => x.GetProducerAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<TenraiValidationException>();
    }
    
    [Fact]
    public async Task GetProducersAsync_PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await _tenrai.GetProducerAsync(1);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Pierrot"));
        results.Data.TotalCount.Should().BeGreaterThan(250);
        results.Data.Established.Should().HaveYear(1979);
    }
    
    [Fact]
    public async Task GetProducersAsync_KyoAniId_ShouldParsePierrot()
    {
        // When
        var results = await _tenrai.GetProducerAsync(2);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Kyoto Animation"));
        results.Data.TotalCount.Should().BeGreaterThan(120);
        results.Data.Established.Should().HaveYear(1985);
        results.Data.About.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetProducerAsync_KyotoAnimation_HasFavorites()
    {
        // When
        var results = await _tenrai.GetProducerAsync(2);

        // Then
        results.Data.Favorites.Should().BeGreaterThan(10_000);
    }
}