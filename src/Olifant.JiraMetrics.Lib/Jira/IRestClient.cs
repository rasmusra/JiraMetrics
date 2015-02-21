using System.Collections.Generic;

namespace HM.OT.JiraMetrics.Lib.Jira
{
    public interface IRestClient
    {
        List<string> GetJsonChunks(string jql);
    }
}