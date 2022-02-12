using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	public class GetRandomCharacterAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomCharacterAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetRandomCharacterAsync_ShouldReturnNotNullCharacter()
		{
			// When
			var character = await _jikan.GetRandomCharacterAsync();

			// Then
			character.Data.Should().NotBeNull();
		}
	}
}