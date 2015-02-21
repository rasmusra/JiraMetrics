using System.Collections.Generic;
using System.IO;

using HM.OT.JiraMetrics.Lib.Jira;

namespace HM.OT.JiraMetrics.Test.Fakes
{
    internal class FakeRestClient : IRestClient
    {
        public List<string> GetJsonChunks(string jql)
        {
            string result;

            switch (jql)
            {
            case "Issues started before and after 2014-07-01":
                result = ReadJsonFile("key in (OFU-2377,OFU-1462)");
                break;

            default:
                result = ReadJsonFile(jql);
                break;
            }

            return new List<string> { result };
        }

        private static string ReadJsonFile(string filename)
        {
            return File.ReadAllText(Path.Combine("Stubs", string.Format("{0}.json", filename)));
        }
    }
}