using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Tenrai.Exceptions;
using Xunit;

namespace Tenrai.Tests.IdsTests
{
	[Collection("TenraiTests")]
	public class GetIdsAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetIdsAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public void IdsResponse_ShouldDeserializeDataArrayIntoCollectionOfIds()
		{
			// Given
			const string json = "{\"data\":[1,5,6,7,8,15,16]}";

			// When
			var response = JsonSerializer.Deserialize<BaseTenraiResponse<ICollection<long>>>(json);

			// Then
			using (new AssertionScope())
			{
				response.Data.Should().HaveCount(7);
				response.Data.Should().ContainInOrder(1L, 5L, 6L, 7L, 8L, 15L, 16L);
			}
		}

		[Fact]
		public void IdsResponse_ShouldDeserializeTotal()
		{
			// Given
			const string json = "{\"total\":7,\"data\":[1,5,6,7,8,15,16]}";

			// When
			var response = JsonSerializer.Deserialize<IdsTenraiResponse>(json);

			// Then
			response.Total.Should().Be(7);
		}

		[Fact]
		public void IdEntriesResponse_ShouldDeserializeTitleAndType()
		{
			// Given
			const string json = "{\"total\":2,\"data\":[{\"mal_id\":1,\"title\":\"Cowboy Bebop\",\"type\":\"TV\"},{\"mal_id\":5,\"title\":\"Cowboy Bebop: Tengoku no Tobira\",\"type\":null}]}";

			// When
			var response = JsonSerializer.Deserialize<IdEntriesTenraiResponse>(json);

			// Then
			using (new AssertionScope())
			{
				response.Total.Should().Be(2);
				response.Data.Should().HaveCount(2);
				response.Data.First().MalId.Should().Be(1);
				response.Data.First().Title.Should().Be("Cowboy Bebop");
				response.Data.First().Type.Should().Be("TV");
				response.Data.Last().Type.Should().BeNull();
			}
		}

		[Fact]
		public async Task GetAnimeIdsAsync_WithoutServerKey_ShouldRequireAuthorization()
		{
			// When (public tier has no Server Key, so the endpoint is gated)
			var func = _tenrai.Awaiting(x => x.GetAnimeIdsAsync());

			// Then
			var exceptionAssertion = await func.Should().ThrowExactlyAsync<TenraiRequestException>();
			using (new AssertionScope())
			{
				var apiError = exceptionAssertion.Which.ApiError;
				apiError.Should().NotBeNull();
				apiError.Status.Should().Be(HttpStatusCode.Unauthorized);
				apiError.Path.Should().Contain("/anime/ids");
			}
		}
	}
}
