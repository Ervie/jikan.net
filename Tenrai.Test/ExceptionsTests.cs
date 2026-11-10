using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests;

public class ExceptionsTests
{
	private readonly ITenrai _tenrai;

	public ExceptionsTests()
	{
		_tenrai = new TenraiClient(new Config.TenraiClientConfiguration { SuppressException = true });
	}

	[Fact]
	public async Task GetMangaAsync_NonExistentId_ShouldDeserializeTenraiErrorEnvelope()
	{
		// Given
		var jikan = new TenraiClient();

		// When
		var func = jikan.Awaiting(x => x.GetMangaAsync(999999999));

		// Then
		var exceptionAssertion = await func.Should().ThrowExactlyAsync<TenraiRequestException>();
		using (new AssertionScope())
		{
			var apiError = exceptionAssertion.Which.ApiError;
			apiError.Should().NotBeNull();
			apiError.Status.Should().Be(HttpStatusCode.NotFound);
			apiError.Type.Should().Be("ResourceNotFoundException");
			apiError.Path.Should().Contain("/manga/999999999");
		}
	}

	[Fact]
	public void GetAnimeAsync_WrongIdDoNotSurpressExceptions_ShouldThrowTenraiRequestExceptionGetAnime()
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetAnimeAsync(2));

		// Then
		func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public void GetMangaAsync_WrongIdDoNotSurpressExceptions_ShouldThrowTenraiRequestExceptionGetManga()
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetMangaAsync(5));

		// Then
		func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}

	[Fact]
	public void GetPerson_WrongIdDoNotSurpressExceptions_ShouldThrowTenraiRequestExceptionGetPerson()
	{
		// When
		var func = _tenrai.Awaiting(x => x.GetPersonAsync(13308));

		// Then
		func.Should().ThrowExactlyAsync<TenraiRequestException>();
	}
}