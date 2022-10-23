using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.ProducerTests;

public class GetProducerAsyncTests
{
    private readonly IJikan _jikan;

    public GetProducerAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetProducerAsync_InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetProducerAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }
    
    [Fact]
    public async Task GetProducersAsync_PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await _jikan.GetProducerAsync(1);

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
        var results = await _jikan.GetProducerAsync(2);

        // Then
        using var _ = new AssertionScope();
        results.Data.Titles.Should().Contain(x => x.Title.Equals("Kyoto Animation"));
        results.Data.TotalCount.Should().BeGreaterThan(120);
        results.Data.Established.Should().HaveYear(1985);
        results.Data.About.Should().NotBeNullOrEmpty();
    }
}