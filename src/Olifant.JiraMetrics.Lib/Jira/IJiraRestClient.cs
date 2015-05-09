using System.Collections.Generic;

namespace Olifant.JiraMetrics.Lib.Jira
{
    public interface IJiraRestClient
    {
        List<string> GetJsonChunks(string jql);
        List<string> GetJsonChunks(JiraProjectQuery query);

    }
}