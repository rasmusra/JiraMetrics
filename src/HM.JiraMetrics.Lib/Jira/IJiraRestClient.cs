using System.Collections.Generic;

namespace HM.JiraMetrics.Lib.Jira
{
    public interface IJiraRestClient
    {
        List<string> GetJsonChunks(string jql);
    }
}