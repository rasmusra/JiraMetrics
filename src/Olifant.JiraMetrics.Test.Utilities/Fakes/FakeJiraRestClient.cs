using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Olifant.JiraMetrics.Lib.Jira;
using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Test.Utilities.Fakes
{
    // TODO: read from mongo instead of files
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
            var result = ReadJsonFile(jql);

            return new List<string> {result};
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