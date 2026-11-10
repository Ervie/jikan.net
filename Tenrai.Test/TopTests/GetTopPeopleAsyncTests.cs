using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.TopTests;

[Collection("TenraiTests")]
public class GetTopPeopleAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetTopPeopleAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetTopPeopleAsync_NoParameters_ShouldParseKanaHanazawa()
	{
		// When
		var top = await _tenrai.GetTopPeopleAsync();

		// Then
		var kana = top.Data.Skip(1).First();
		using (new AssertionScope())
		{
			kana.Name.Should().Be("Kana Hanazawa");
			kana.GivenName.Should().Be("香菜");
			kana.FamilyName.Should().Be("花澤");
			kana.MalId.Should().Be(185);
			kana.Birthday.Value.Year.Should().Be(1989);
			kana.MemberFavorites.Should().BeGreaterThan(95000);
		}
	}

	[Fact]
	public async Task GetTopPeopleAsync_NoParameters_ShouldParseHiroshiKamiya()
	{
		// When
		var top = await _tenrai.GetTopPeopleAsync();

		// Then
		var kamiya = top.Data.First();
		using (new AssertionScope())
		{
			kamiya.Name.Should().Be("Hiroshi Kamiya");
			kamiya.GivenName.Should().Be("浩史");
			kamiya.FamilyName.Should().Be("神谷");
			kamiya.MalId.Should().Be(118);
			kamiya.Birthday.Value.Year.Should().Be(1975);
			kamiya.MemberFavorites.Should().BeGreaterThan(99000);
		}
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(-1)]
	[InlineData(0)]
	public async Task GetTopPeopleAsync_InvalidPage_ShouldThrowValidationException(int page)
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetTopPeopleAsync(page));

		// Then
		await func.Should().ThrowExactlyAsync<TenraiValidationException>();
	}

	[Fact]
	public async Task GetTopPeopleAsync_SecondPage_ShouldFindMaasakiYuasa()
	{
		// When
		var top = await _tenrai.GetTopPeopleAsync(2);

		// Then
		top.Data.Select(x => x.Name).Should().Contain("Masaaki Yuasa");
	}

	[Fact]
	public async Task GetTopPeopleAsync_Limit5_ReturnsFive()
	{
		// When
		var top = await _tenrai.GetTopPeopleAsync(new PagingConfig { Limit = 5 });

		// Then
		top.Data.Should().HaveCount(5);
	}
}