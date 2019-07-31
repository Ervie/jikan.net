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
		public async Task SearchMethods_EmptyQuery_ShouldReturnNoResult()
		{
			var nullAnime = await jikan.SearchAnime("");
			var nullManga = await jikan.SearchManga("");
			var nullPerson = await jikan.SearchPerson("");
			var nullCharacter = await jikan.SearchCharacter("");

			Assert.Empty(nullAnime.Results);
			Assert.Empty(nullManga.Results);
			Assert.Empty(nullPerson.Results);
			Assert.Empty(nullCharacter.Results);
		}
	}
}