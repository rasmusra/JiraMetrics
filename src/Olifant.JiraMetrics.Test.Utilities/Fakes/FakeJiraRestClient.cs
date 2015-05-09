using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson.IO;
using Olifant.JiraMetrics.Lib.Jira;

namespace Olifant.JiraMetrics.Test.Utilities.Fakes
{
    public class FakeJiraRestClient : IJiraRestClient
    {
        private readonly string _stubDirectory;

        public FakeJiraRestClient(string stubDirectory)
        {
            _stubDirectory = stubDirectory;
        }

        public List<string> GetJsonChunks(string jql)
        {
            return JqlLookup(jql);
        }

        public List<string> GetJsonChunks(JiraProjectQuery project)
        {
            // read all stub files with file names matching project name
            // TODO: this feels really messy
            var pattern = string.Format("key={0}*.json", project.ProjectName);
            var allIssues = ReadMatchingJsonFiles(pattern).ToList();
            return allIssues;
        }

        private List<string> JqlLookup(string jql)
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

            return new List<string> {result};
        }

        public string GetStatuses()
        {
            return File.ReadAllText(Path.Combine(_stubDirectory, "statuses.json"));
        }

        public string ReadJsonFile(string filename)
        {
            return File.ReadAllText(Path.Combine(_stubDirectory, string.Format("{0}.json", filename)));
        }

        public IList<string> ReadAllJsonFiles()
        {
            var stubs = Directory.GetFiles(_stubDirectory, "key*.json");
            var jsonTexts = stubs.Select(File.ReadAllText).ToList();
            return jsonTexts;
        }

        public bool MatchingFileExists(string pattern)
        {
            var stubs = Directory.GetFiles(_stubDirectory, pattern);
            return stubs.Any();
        }

        private IList<string> ReadMatchingJsonFiles(string pattern)
        {
            var stubs = Directory.GetFiles(_stubDirectory, pattern);
            var jsonTexts = stubs.Select(File.ReadAllText).ToList();
            return jsonTexts;
        }
    }
}