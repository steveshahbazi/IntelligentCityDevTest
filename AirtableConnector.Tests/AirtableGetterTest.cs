using AirtableConnector.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AirtableConnector.Tests
{
	public class AirtableGetterTest
	{
		AirtableGetter Getter { get; set; } = new AirtableGetter("apphruxl9mXWH7QJJ", "API_KEY_FILEPATH");

		[Fact]
		public async void RetrieveDataFromProjectsTableTest()
		{
			Dictionary<string, List<string>> data = await Getter.RetrieveDataFromTablesAsync("Projects");
			//Assert.NotEmpty(data);
			Assert.False(true);
		}

		[Fact]
		public async Task RetrieveDataFromClientsTableTestAsync()
		{
			Dictionary<string, List<string>> data = await Getter.RetrieveDataFromTablesAsync("Client");
			Assert.NotEmpty(data);
		}

		[Fact]
		public async Task QueryProjectsByCityTest()
		{
			Dictionary<string, List<string>> data = await Getter.QueryProjectsByCity("Vancouver");
			Assert.True(data.Count == 6);
		}
	}
}
