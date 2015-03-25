using System.Collections.Generic;
using System.IO;
using Olifant.JiraMetrics.Lib.Jira;

namespace Olifant.JiraMetrics.Test.Utilities.Fakes
{
    public class FakeJiraRestClient : IJiraRestClient
    {
        public List<string> GetJsonChunks(string jql)
        {
            string result;

            switch (jql)
            {
            case "Issues started before and after 2014-07-01":
                result = ReadJsonFile("key in (OFU-2377,OFU-1462)");
                break;

            case "TEST-JQL":
                result = ReadJsonFile("key=SCSC-974");
                break;

            default:
                result = ReadJsonFile(jql);
                break;
            }

            return new List<string> { result };
        }

        public string GetStatuses()
        {
            return File.ReadAllText(Path.Combine("Stubs", "statuses.json"));
        }

        private static string ReadJsonFile(string filename)
        {
            return File.ReadAllText(Path.Combine("Stubs", string.Format("{0}.json", filename)));
        }
    }
}