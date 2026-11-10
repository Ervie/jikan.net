using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaExternalLinksAsyncTests
{

	private readonly ITenrai _tenrai;

	public GetMangaExternalLinksAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetMangaExternalLinksAsync_InvalidId_ShouldThrowValidationException(long malId)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaExternalLinksAsync(malId));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetMangaExternalLinksAsync_MonsterId_ShouldReturnMonsterLinks()
	{
		// When
		var links = await _tenrai.GetMangaExternalLinksAsync(1);

		// Then
		links.Data.Should().Contain(x => x.Name.Equals("Wikipedia") && x.Url.Equals("http://ja.wikipedia.org/wiki/MONSTER"));
	}
}