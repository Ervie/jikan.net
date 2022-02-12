using FluentAssertions;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserAboutAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserAboutAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserAboutAsync_nvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserAboutAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserAboutAsync_Ervelan_ShouldParseErvelanAbout()
		{
			// When
			var user = await _jikan.GetUserAboutAsync("Ervelan");

			// Then
			user.Data.About.Should().Contain("Welcome to my profile!");
		}
	}
}