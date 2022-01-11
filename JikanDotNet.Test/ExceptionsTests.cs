using FluentAssertions;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class ExceptionsTests
	{
		private readonly IJikan _jikan;

		public ExceptionsTests()
		{
			_jikan = new Jikan(new Config.JikanClientConfiguration { SuppressException = true });
		}

		[Fact]
		public void GetAnimeAsync_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetAnime()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetAnimeAsync(2));

			// Then
			func.Should().ThrowExactlyAsync<JikanRequestException>();
		}

		[Fact]
		public void GetMangaAsync_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetManga()
		{
			// When
			var func = _jikan.Awaiting(x => x.GetMangaAsync(5));

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