using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.GetRandomTests;

[Collection("TenraiTests")]
public class GetRandomPersonAsyncTests
{
	private readonly ITenrai _tenrai;

	public GetRandomPersonAsyncTests(TenraiFixture tenraiFixture)
	{
		_tenrai = tenraiFixture.TenraiClient;
	}

	[Fact]
	public async Task GetRandomPersonAsync_ShouldReturnNotNullPerson()
	{
		// When
		var person = await _tenrai.GetRandomPersonAsync();

		// Then
		person.Data.Should().NotBeNull();
	}
}