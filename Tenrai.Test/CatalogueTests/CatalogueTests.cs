using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.CatalogueTests;

[Collection("TenraiTests")]
public class CatalogueTests
{
	private readonly ITenrai _tenrai;

	public CatalogueTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetUnapprovedAnimeAsync_AllUnapproved()
	{
		// When
		var anime = await _tenrai.GetUnapprovedAnimeAsync();

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().NotBeEmpty();
		anime.Data.Should().OnlyContain(entry => !entry.Approved);
	}

	[Fact]
	public async Task GetDeletedAnimeAsync_ByMembersDesc_IsSorted()
	{
		// Given
		var config = new DeletedSearchConfig { OrderBy = DeletedOrderBy.Members, SortDirection = SortDirection.Descending };

		// When
		var anime = await _tenrai.GetDeletedAnimeAsync(config);

		// Then
		using var _ = new AssertionScope();
		anime.Data.Should().NotBeEmpty();
		anime.Data.Select(entry => entry.Members).Should().BeInDescendingOrder();
	}

	[Fact]
	public async Task GetAdaptedMangaAsync_FirstPage_HasTotal()
	{
		// When
		var manga = await _tenrai.GetAdaptedMangaAsync();

		// Then
		using var _ = new AssertionScope();
		manga.Data.Should().HaveCount(25);
		manga.Pagination.Items.Total.Should().BeGreaterThan(4000);
	}
}
