using FluentAssertions;
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
			// When
			var nullAnime = await _jikan.SearchAnime("");

			// Then
			nullAnime.Results.Should().BeEmpty();
		}

		[Fact]
		public async Task SearchMaga_EmptyQuery_ShouldReturnNoResult()
		{
			// When
			var nullManga = await _jikan.SearchManga("");

			// Then
			nullManga.Results.Should().BeEmpty();
		}

		[Fact]
		public async Task SearchPerson_EmptyQuery_ShouldReturnNoResult()
		{
			// When
			var nullPerson = await _jikan.SearchPerson("");

			// Then
			nullPerson.Results.Should().BeEmpty();
		}

		[Fact]
		public void SearchCharacter_EmptyQuery_ShouldThrowException()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => _jikan.SearchCharacter(""));
		}
	}
}