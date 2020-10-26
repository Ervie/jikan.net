using FluentAssertions;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class PersonExtendedTests
	{
		private readonly IJikan _jikan;

		public PersonExtendedTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetPersonPictures_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<PersonPictures>> func = _jikan.Awaiting(x => x.GetPersonPictures(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetPersonPictures_WakamotoId_ShouldParseNorioWakamotoImages()
		{
			// Given
			var norioWakamoto = await _jikan.GetPersonPictures(84);

			// Then
			norioWakamoto.Pictures.Should().HaveCount(4);
		}
	}
}