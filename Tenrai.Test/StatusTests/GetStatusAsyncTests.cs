using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tenrai.Tests.StatusTests
{
	[Collection("TenraiTests")]
	public class GetStatusAsyncTests
	{
		private readonly ITenrai _tenrai;

		public GetStatusAsyncTests(TenraiFixture tenraiFixture)
		{
			_tenrai = tenraiFixture.TenraiClient;
		}

		[Fact]
		public async Task GetStatusAsync_NoParameter_ShouldParseStatusSnapshot()
		{
			// When
			var status = await _tenrai.GetStatusAsync();

			// Then
			using (new AssertionScope())
			{
				status.Should().NotBeNull();
				status.ApiVersion.Should().BeGreaterThan(0);
				status.GeneratedAt.Should().NotBeNull();
				status.Page.Should().NotBeNull();
				status.Page.Url.Should().NotBeNullOrWhiteSpace();
				status.Checker.Should().NotBeNull();
				status.Checker.StaleAfterSeconds.Should().BeGreaterThan(0);
				status.Services.Should().NotBeNullOrEmpty();
				status.ScheduledMaintenances.Should().NotBeNull();
			}
		}

		[Fact]
		public async Task GetStatusAsync_NoParameter_ShouldParseTenraiService()
		{
			// Given
			var validStatuses = new[] { "operational", "degraded", "down", "unknown", "maintenance" };

			// When
			var status = await _tenrai.GetStatusAsync();

			// Then
			var tenrai = status.Services.FirstOrDefault(service => service.Id == "tenrai");
			using (new AssertionScope())
			{
				tenrai.Should().NotBeNull();
				tenrai.Name.Should().NotBeNullOrWhiteSpace();
				tenrai.Status.Should().BeOneOf(validStatuses);
				tenrai.HomepageUrl.Should().NotBeNullOrWhiteSpace();
				tenrai.OutageMinutes90d.Should().BeGreaterOrEqualTo(0);
				tenrai.DailyOutageMinutes90d.Should().NotBeNull();
				tenrai.DailyDegradedMinutes90d.Should().NotBeNull();
			}
		}
	}
}
