using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class SearchTestClass
	{
		private readonly IJikan jikan;

		public SearchTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public async Task SearchAnime_EmptyQuery_ShouldReturnNoResult()
		{
			var nullAnime = await jikan.SearchAnime("");

			Assert.Empty(nullAnime.Results);
		}

		[Fact]
		public async Task SearchMaga_EmptyQuery_ShouldReturnNoResult()
		{
			var nullManga = await jikan.SearchManga("");

			Assert.Empty(nullManga.Results);
		}

		[Fact]
		public async Task SearchPerson_EmptyQuery_ShouldReturnNoResult()
		{
			var nullPerson = await jikan.SearchPerson("");

			Assert.Empty(nullPerson.Results);
		}

		[Fact]
		public void SearchCharacter_EmptyQuery_ShouldThrowException()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.SearchCharacter(""));
		}
	}
}