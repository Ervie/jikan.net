using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	public class GetRandomUserAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomUserAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetRandomUserAsync_ShouldReturnNotNullUser()
		{
			// When
			var user = await _jikan.GetRandomUserAsync();

			// Then
			user.Data.Should().NotBeNull();
		}
	}
}