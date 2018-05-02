using JikanDotNet.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
		public void ShouldThrowJikanRequestExceptionGetAnime()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetAnime(2));
		}

		[Fact]
		public void ShouldThrowJikanRequestExceptionGetManga()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetManga(5));
		}

		[Fact]
		public void ShouldThrowJikanRequestExceptionGetPerson()
		{
			Assert.ThrowsAnyAsync<JikanRequestException>(() => jikan.GetPerson(13308));
		}
	}
}
