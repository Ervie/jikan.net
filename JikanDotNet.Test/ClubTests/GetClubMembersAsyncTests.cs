using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.ClubTests
{
	public class GetClubMembersAsyncTests
	{
		private readonly IJikan _jikan;

		public GetClubMembersAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetClubMembersAsync_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetClubMembersAsync(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetClubMembersAsync_BebopId_ShouldParseCowboyBebopClubMemberList()
		{
			// When
			var club = await _jikan.GetClubMembersAsync(1);

			// Then
			using (new AssertionScope())
			{
				club.Data.Should().NotBeEmpty();
				club.Data.First().Username.Should().Be("--Pascal--");
				club.Pagination.HasNextPage.Should().BeFalse();
				club.Pagination.LastVisiblePage.Should().Be(1);
			}
		}

		[Theory]
		[InlineData(int.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetClubMembersAsync_InvalidPage_ShouldThrowValidationException(int page)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetClubMembersAsync(1, page));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetClubMembersAsync_BebopIdSecondPage_ShouldParseCowboyBebopClubMemberListPaged()
		{
			// When
			var members = await _jikan.GetClubMembersAsync(1, 2);

			// Then
			using (new AssertionScope())
			{
				members.Data.Should().NotBeEmpty();
				members.Data.Should().HaveCount(36);
			}
		}
	}
}
