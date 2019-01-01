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
		public void ShouldParseCowboyBebopClub()
		{
			Club club = Task.Run(() => jikan.GetClub(1)).Result;

			Assert.NotNull(club);
			Assert.Equal("Anime", club.Category);
			Assert.Equal("public", club.Type);
			Assert.Equal(25, club.PicturesCount);
			Assert.Equal(2, club.MangaRelations.Count);
			Assert.Equal("Cowboy Bebop", club.AnimeRelations.First().Name);
		}

		[Fact]
		public void ShouldParseAnimeCafeClub()
		{
			Club club = Task.Run(() => jikan.GetClub(73113)).Result;

			Assert.NotNull(club);
			Assert.Equal("Anime", club.Category);
			Assert.Equal("public", club.Type);
			Assert.Empty(club.CharacterRelations);
			Assert.Equal("Fehr", club.Staff.First().Name);
		}

		[Fact]
		public void ShouldParseCowboyBebopClubMemberList()
		{
			ClubMembers club = Task.Run(() => jikan.GetClubMembers(1)).Result;

			Assert.NotEmpty(club.Members);

			ClubMember firstMember = club.Members.First();

			Assert.Equal("-alquimista-", firstMember.Username);
		}

		[Fact]
		public void ShouldParseCowboyBebopClubMemberListPaged()
		{
			ClubMembers club = Task.Run(() => jikan.GetClubMembers(1, 2)).Result;

			Assert.NotEmpty(club.Members);
			Assert.Equal(36, club.Members.Count);
		}
	}
}