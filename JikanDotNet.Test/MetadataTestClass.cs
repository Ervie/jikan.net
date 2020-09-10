using System;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MetadataTestClass
	{
		private readonly IJikan _jikan;

		public MetadataTestClass()
		{
			_jikan = new Jikan(true);
		}

		[Fact]
		public async Task GetStatusMetadata_NoParameter_ShouldParseStatusMetadata()
		{
			StatusMetadata statusMetadata = await _jikan.GetStatusMetadata();

			Assert.NotNull(statusMetadata);
			Assert.True(int.Parse(statusMetadata.TotalConnectionsReceived) > 1000000);
		}
	}
}