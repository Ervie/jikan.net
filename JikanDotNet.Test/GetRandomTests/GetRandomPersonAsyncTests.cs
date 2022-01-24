using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests.GetRandomTests
{
	public class GetRandomPersonAsyncTests
	{
		private readonly IJikan _jikan;

		public GetRandomPersonAsyncTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetRandomPersonAsync_ShouldReturnNotNullPerson()
		{
			// When
			var person = await _jikan.GetRandomPersonAsync();

			// Then
			person.Data.Should().NotBeNull();
		}
	}
}