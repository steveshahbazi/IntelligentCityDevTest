using AirtableConnector.Core;
using NUnit.Framework;
using System.Diagnostics.Metrics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace TestProjectNUnit
{
    public class Tests
    {
        //since this is for test only purpose I've added the secrets here; however, it should be properly encoded either in app_settings or coming from a database, or...
        AirtableGetter Getter { get; set; } = new AirtableGetter("apphruxl9mXWH7QJJ", @"..\..\..\Resources\secrets.json");

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task CheckRetrieveDataFromProjectsTableTestIsNotNullOrEmpty()
        {
            Dictionary<string, List<string>> data = await Getter.RetrieveDataFromTablesAsync("Projects");
            Assert.IsNotNull(data);
            Assert.IsNotEmpty(data);
        }

        [Test]
        public async Task CheckRetrieveDataFromProjectsTableTestReturnsExpectedRecordCount()
        {
            Dictionary<string, List<string>> data = await Getter.RetrieveDataFromTablesAsync("Projects");
            int resultSize = 15;
            Assert.That(data.Count.Equals(resultSize), Is.True, $"The result count should be {resultSize}");
        }

        [Test]
        //this test will fail as the result records are mot sorted based on what we see on sandbox
        //this can be fixed either by investigating the given library or getting approval from the business owner if the order is correct
        //we can also make more test cases to check other records as this one only checks one record
        public async Task CheckRetrieveDataFromProjectsTableTestReturnsCorrectData()
        {
            Dictionary<string, List<string>> data = await Getter.RetrieveDataFromTablesAsync("Projects");
            string[] firstRecord = new string[] { "John Doe", "Vancouver", "5000", "$200.00", "25000", "$5, 000, 000.00" };
            string key = "rec6iPAxE4X0vJQGL";
            for (int i = 0; i < firstRecord.Length; i++)
            {
                Assert.That(data[key][i].Equals(firstRecord[i]), Is.True, $"The result does not match what expected, {data[key][i]} is received while {firstRecord[i]} expected.");
            }
        }
    }
}