using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.ProducerTests;

[Collection("TenraiTests")]
public class GetProducerExternalLinksAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetProducerExternalLinksAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetProducerExternalLinksAsync_InvalidId_ShouldThrowValidationException(long id)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetProducerExternalLinksAsync(id));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetProducerExternalLinksAsync_PierrotId_ShouldParsePierrot()
	{
		// When
		var results = await _tenrai.GetProducerExternalLinksAsync(1);

		// Then
		using var _ = new AssertionScope();
		results.Data.Should().HaveCountGreaterOrEqualTo(5);
		results.Data.Should().Contain(x => x.Name.Equals("pierrot.jp") && x.Url.Equals("http://pierrot.jp/"));
	}
}