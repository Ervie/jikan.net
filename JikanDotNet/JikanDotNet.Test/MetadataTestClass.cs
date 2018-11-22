using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace JikanDotNet.Tests
{
	public class MetadataTestClass
	{
		private readonly IJikan jikan;

		public MetadataTestClass()
		{
			jikan = new Jikan(true);
		}

		[Fact]
		public void ShouldParseStatusMetadata()
		{
			StatusMetadata statusMetadata = Task.Run(() => jikan.GetStatusMetadata()).Result;

			Assert.NotNull(statusMetadata);
			Assert.True(Int32.Parse(statusMetadata.TotalConnectionsReceived) > 10000000);
		}
	}
}