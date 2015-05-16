using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;

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
            // CHEAT: deserialize all json stubs for checking which issues that is updated.
            // Then read the updated issues again from json.
            // This will mimic Jira servers ability to filter on updated date.
            var jsonChunks = ReadAllJsonFiles();
            var updatedIssues = jsonChunks
                .SelectMany(chunk => JsonConvert.DeserializeObject<Issues>(chunk).IssueList)
                .Where(issue => issue.Fields.Project.Name==project.ProjectName)
                .Where(issue => DateTime.Parse(issue.Fields.Updated) > project.UpdateDate)
                .Select(issue => string.Format("*{0}*", issue.Key))
                .SelectMany(ReadMatchingJsonFiles)
                .Distinct()
                .ToList();
            return updatedIssues;
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

        public IList<string> ReadMatchingJsonFiles(string pattern)
        {
            var stubs = Directory.GetFiles(_stubDirectory, pattern);
            var jsonTexts = stubs.Select(File.ReadAllText).ToList();
            return jsonTexts;
        }
    }
}