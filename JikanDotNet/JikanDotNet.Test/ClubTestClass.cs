using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ClubTestClass
	{
		private readonly IJikan jikan;

		public ClubTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetClub_BebopId_ShouldParseCowboyBebopClub()
		{
			Club club = await jikan.GetClub(1);

			Assert.NotNull(club);
			Assert.Equal("Anime", club.Category);
			Assert.Equal("public", club.Type);
			Assert.Equal(25, club.PicturesCount);
			Assert.Equal(2, club.MangaRelations.Count);
			Assert.Equal("Cowboy Bebop", club.AnimeRelations.First().Name);
		}

		[Fact]
		public async Task GetClub_AnimeCafeId_ShouldParseAnimeCafeClub()
		{
			Club club = await jikan.GetClub(73113);

			Assert.NotNull(club);
			Assert.Equal("Anime", club.Category);
			Assert.Equal("public", club.Type);
			Assert.Empty(club.CharacterRelations);
			Assert.Equal("Fehr", club.Staff.First().Name);
		}

		[Fact]
		public async Task GetClubMembers_BebopId_ShouldParseCowboyBebopClubMemberList()
		{
			ClubMembers club = await jikan.GetClubMembers(1);

			Assert.NotEmpty(club.Members);

			ClubMember firstMember = club.Members.First();

			Assert.Equal("-alquimista-", firstMember.Username);
		}

		[Fact]
		public async Task GetClubMembers_BebopIdSecondPage_ShouldParseCowboyBebopClubMemberListPaged()
		{
			ClubMembers club = await jikan.GetClubMembers(1, 2);

			Assert.NotEmpty(club.Members);
			Assert.Equal(36, club.Members.Count);
		}
	}
}