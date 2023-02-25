using AirtableApiClient;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AirtableConnector.Core
{
	public class AirtableGetter
	{
		public AirtableBase Base { get; set; }

		public AirtableGetter(string baseId, string apiKeyFile)
		{
			Base = new AirtableBase(ReadApiKey(apiKeyFile), baseId);
		}

		private static string ReadApiKey(string apiKeyFile, string key = "Airtable")
		{
			// Read the entire file
			string json = File.ReadAllText(apiKeyFile);

			// Parse the JSON data into a C# object
			JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			JsonObject data = JsonSerializer.Deserialize<JsonObject>(json, options);
			return data[key].ToString();
		}

		public async Task<Dictionary<string, List<string>>> RetrieveDataFromTablesAsync(string tableName)
		{
            //I need more information on what you actuallu needs to be returned
            //I assume you need a dictionary with key equals to Id of a record
            //which comes from Base.ListRecords records and the values are from the fields inside each record
            Dictionary<string, List<string>> data = new();
			var result = await Base.ListRecords(tableName);
			if (result.Success)
			{
				foreach (var item in result.Records)
				{
					List<string> record = new List<string>();
					foreach (var rec in item.Fields)
					{
						record.Add(rec.Value.ToString());
					}
					data.Add(item.Id, record);
				}
			}
			return data;
		}

		public async Task<Dictionary<string, List<string>>> QueryProjectsByCity(string cityName)
		{
			Dictionary<string, List<string>> data = new();

            var result = await RetrieveDataFromTablesAsync("Projects");
			data = result.Where(kvp => kvp.Value.Contains(cityName)).ToDictionary(x => x.Key, x => x.Value);
            return data;
        }
	}
}
