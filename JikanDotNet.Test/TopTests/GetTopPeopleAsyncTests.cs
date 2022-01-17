using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.TopTests
{
	public class GetTopPeopleAsyncTests
	{
		private readonly IJikan _jikan;

		public GetTopPeopleAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetTopPeopleAsync_NoParameters_ShouldParseKanaHanazawa()
		{
			// When
			var top = await _jikan.GetTopPeopleAsync();

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
			var top = await _jikan.GetTopPeopleAsync();

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
			var func = _jikan.Awaiting(x => x.GetTopPeopleAsync(page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetTopPeopleAsync_SecondPage_ShouldFindMaasakiYuasa()
		{
			// When
			var top = await _jikan.GetTopPeopleAsync(2);

			// Then
			top.Data.Select(x => x.Name).Should().Contain("Masaaki Yuasa");
		}
	}
}