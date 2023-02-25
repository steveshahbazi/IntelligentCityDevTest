using AirtableConnector.Core;
using NUnit.Framework;
using System.Diagnostics.Metrics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace TestProjectNUnit
{
    public class TestProjectsLocatedInCertainCity
    {
        //since this is for test only purpose I've added the secrets here; however, it should be properly encoded either in app_settings or coming from a database, or...
        AirtableGetter Getter { get; set; } = new AirtableGetter("apphruxl9mXWH7QJJ", @"..\..\..\Resources\secrets.json");
        readonly string city = "Seattle";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CheckRetrieveDataFromProjectsTableTestIsNotNullOrEmptyForSeattle()
        {
            Dictionary<string, List<string>> data = await Getter.QueryProjectsByCity(city);
            Assert.IsNotNull(data);
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task CheckRetrieveDataForSeattleExpectedRecordCount()
        {
            Dictionary<string, List<string>> data = await Getter.QueryProjectsByCity(city);
            int resultSize = 5;
            Assert.That(data.Count.Equals(resultSize), Is.True, $"The result count should be {resultSize}");
        }

        [Test]
        public async Task CheckRetrieveDataForSeattleCorrectData()
        {
            Dictionary<string, List<string>> data = await Getter.QueryProjectsByCity(city);
            foreach (var item in data)
            {
                var val = item.Value;
                Assert.That(val.Contains(city), Is.True, $"The result does not include the expected city of {city}");
            }
        }
    }
}