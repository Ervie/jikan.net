using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class SearchTestClass
	{
		private readonly IJikan _jikan;

		public SearchTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task SearchAnime_EmptyQuery_ShouldReturnNoResult()
		{
			var nullAnime = await _jikan.SearchAnime("");

			Assert.Empty(nullAnime.Results);
		}

		[Fact]
		public async Task SearchMaga_EmptyQuery_ShouldReturnNoResult()
		{
			var nullManga = await _jikan.SearchManga("");

			Assert.Empty(nullManga.Results);
		}

		[Fact]
		public async Task SearchPerson_EmptyQuery_ShouldReturnNoResult()
		{
			var nullPerson = await _jikan.SearchPerson("");

			Assert.Empty(nullPerson.Results);
		}

		[Fact]
		public void SearchCharacter_EmptyQuery_ShouldThrowException()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.SearchCharacter(""));
		}
	}
}