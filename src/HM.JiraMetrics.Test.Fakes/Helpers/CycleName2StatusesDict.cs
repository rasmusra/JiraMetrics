using System;
using System.Collections.Generic;

namespace HM.JiraMetrics.Test.Fakes.Helpers
{
    public class CycleName2StatusesDict : Dictionary<string, string[]>
    {
        private static readonly CycleName2StatusesDict Instance = new CycleName2StatusesDict();

        private CycleName2StatusesDict()
        {
            this["dev"] = new[]
                              {
                                  "Implement", "Build & Configure", "Implementing", "Building & Configuring",
                                  "In Progress", "Review"
                              };
            this["test"] = new[] { "System Test", "Test", "Ready for Test", "Deployed In Test", "Testing", "System Testing" };
            this["backlog"] = new[] { "Open", "Reopened" };
            this["full process"] = new[]
                                       {
                                           "Describe Requirement", "Describing Requirement", "Design Architecture",
                                           "Designing Architecture", "Implement", "Build & Configure", "Implementing",
                                           "Building & Configuring", "In Progress", "Review", "System Test", "Test",
                                           "Ready for Test", "Deployed To Test", "Testing", "System Testing",
                                           "System Test Done"
                                       };
        }

        public static string[] Lookup(string index)
        {
            index = index.ToLower();

            if (!Instance.ContainsKey(index))
            {
                throw new NotImplementedException(index);
            }

            return Instance[index];
        }
    }
}