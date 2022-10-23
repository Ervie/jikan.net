using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests.ProducerTests;

public class GetProducerExternalLinksAsyncTests
{
    private readonly IJikan _jikan;

    public GetProducerExternalLinksAsyncTests()
    {
        _jikan = new Jikan();
    }

    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetProducerExternalLinksAsync_InvalidId_ShouldThrowValidationException(long id)
    {
        // When
        var func = _jikan.Awaiting(x => x.GetProducerExternalLinksAsync(id));

        // Then
        await func.Should().ThrowExactlyAsync<JikanValidationException>();
    }
    
    [Fact]
    public async Task GetProducerExternalLinksAsync_PierrotId_ShouldParsePierrot()
    {
        // When
        var results = await _jikan.GetProducerExternalLinksAsync(1);

        // Then
        using var _ = new AssertionScope();
        results.Data.Should().HaveCountGreaterOrEqualTo(5);
        results.Data.Should().Contain(x => x.Name.Equals("pierrot.jp") && x.Url.Equals("http://pierrot.jp/\r"));
    }
}