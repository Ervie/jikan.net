using JikanDotNet.Exceptions;
using Xunit;

namespace JikanDotNet.Tests
{
    public class ExceptionsTestClass
    {
		private readonly IJikan jikan;

		public ExceptionsTestClass()
		{
			jikan = new Jikan(true, false);
		}

		[Fact]
		public void GetAnime_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetAnime()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetAnime(2));
		}

		[Fact]
		public void GetManga_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetManga()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetManga(5));
		}

		[Fact]
		public void GetPerson_WrongIdDoNotSurpressExceptions_ShouldThrowJikanRequestExceptionGetPerson()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetPerson(13308));
		}
	}
}
