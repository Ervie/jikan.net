using FluentAssertions;
using FluentAssertions.Execution;
using JikanDotNet.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.UserTests
{
	public class GetUserProfileAsyncTests
	{
		private readonly IJikan _jikan;

		public GetUserProfileAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData("\n\n\t    \t")]
		public async Task GetUserProfileAsync_InvalidUsername_ShouldThrowValidationException(string username)
		{
			// When
			var func = _jikan.Awaiting(x => x.GetUserProfileAsync(username));

			// Then
			await func.Should().ThrowExactlyAsync<JikanValidationException>();
		}

		[Fact]
		public async Task GetUserProfileAsync_Ervelan_ShouldParseErvelanProfile()
		{
			// When
			var user = await _jikan.GetUserProfileAsync("Ervelan");

			// Then
			using (new AssertionScope())
			{
				user.Should().NotBeNull();
				user.Data.Username.Should().Be("Ervelan");
				user.Data.MalId.Should().Be(289183);
				user.Data.Joined.Value.Year.Should().Be(2010);
				user.Data.Birthday.Value.Year.Should().Be(1993);
				user.Data.Gender.Should().Be("Male");
			}
		}

		[Fact]
		public async Task GetUserProfile_Nekomata1037_ShouldParseNekomataProfile()
		{
			// When
			var user = await _jikan.GetUserProfileAsync("Nekomata1037");

			// Then
			using (new AssertionScope())
			{
				user.Data.Should().NotBeNull();
				user.Data.Username.Should().Be("Nekomata1037");
				user.Data.MalId.Should().Be(4901676);
				user.Data.Joined.Value.Year.Should().Be(2015);
			}
		}
	}
}