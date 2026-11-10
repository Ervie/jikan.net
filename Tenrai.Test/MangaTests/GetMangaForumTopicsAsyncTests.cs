using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Tenrai.Tests.MangaTests;

[Collection("TenraiTests")]
public class GetMangaForumTopicsAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetMangaForumTopicsAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetMangaForumTopicsAsync_BerserkByChapterDesc_IsSorted()
	{
		// When
		var berserk = await _tenrai.GetMangaForumTopicsAsync(2, new MangaForumSearchConfig
		{
			OrderBy = MangaForumOrderBy.ChapterNumber,
			SortDirection = SortDirection.Descending
		});

		// Then
		var chapters = berserk.Data.Select(x => double.Parse(x.Title.Split(' ')[2], CultureInfo.InvariantCulture)).ToList();
		using var _ = new AssertionScope();
		berserk.Data.Should().OnlyContain(x => x.Title.StartsWith("Berserk Chapter "));
		chapters.Should().BeInDescendingOrder();
	}
}
