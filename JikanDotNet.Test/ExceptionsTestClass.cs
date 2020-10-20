using FluentAssertions;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
    public class ExceptionsTestClass
    {
		private readonly IJikan _jikan;

		public ExceptionsTestClass()
		{
			_jikan = new Jikan(true, false);
		}

		[Fact]
		public void GetAnime_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetAnime()
		{
			// When
			Func<Task<Anime>> func = _jikan.Awaiting(x => x.GetAnime(2));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void GetManga_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetManga()
		{
			// When
			Func<Task<Manga>> func = _jikan.Awaiting(x => x.GetManga(5));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void GetPerson_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetPerson()
		{
			// When
			Func<Task<Person>> func = _jikan.Awaiting(x => x.GetPerson(13308));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}
	}
}
