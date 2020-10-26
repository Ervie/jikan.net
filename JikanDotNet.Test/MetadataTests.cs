using FluentAssertions;
using FluentAssertions.Execution;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MetadataTests
	{
		private readonly IJikan _jikan;

		public MetadataTests()
		{
			_jikan = new Jikan();
		}

		[Fact]
		public async Task GetStatusMetadata_NoParameter_ShouldParseStatusMetadata()
		{
			// When
			var statusMetadata = await _jikan.GetStatusMetadata();

			// Then
			using (new AssertionScope())
			{
				statusMetadata.Should().NotBeNull();
				int.Parse(statusMetadata.TotalConnectionsReceived).Should().BeGreaterThan(1000000);
			}
		}
	}
}