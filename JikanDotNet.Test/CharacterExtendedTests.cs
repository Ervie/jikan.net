using FluentAssertions;
using JikanDotNet.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class CharacterExtendedTests
	{
		private readonly IJikan _jikan;

		public CharacterExtendedTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(long.MinValue)]
		[InlineData(-1)]
		[InlineData(0)]
		public async Task GetCharacterPictures_InvalidId_ShouldThrowValidationException(long malId)
		{
			// When
			Func<Task<CharacterPictures>> func = _jikan.Awaiting(x => x.GetCharacterPictures(malId));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetCharacterPictures_SharoId_ShouldParseKirimaSharoImages()
		{
			// When
			var kirimaSharo = await _jikan.GetCharacterPictures(94947);

			// Then
			kirimaSharo.Pictures.Should().HaveCount(9);
		}
	}
}