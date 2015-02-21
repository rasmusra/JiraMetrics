using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Olifant.JiraMetrics.Lib.Jira
{
    public class ChunkedJiraRestClient : IJiraRestClient
    {
        private readonly int chunkSizeInDays;

        public ChunkedJiraRestClient(int chunkSizeInDays)
        {
            this.chunkSizeInDays = chunkSizeInDays;
        }

        public List<string> GetJsonChunks(string jql)
        {
            var result = new List<string>();
            var startDate = GetFirstIssuesStartDate(jql);

            if (!startDate.HasValue)
            {
                return result;
            }

            while (startDate < DateTime.Now)
            {
                var jqlWithChunkFilter = JqlChunkHelper.AppendChunkFilter(jql, startDate.Value, chunkSizeInDays);
                Debug.Write(string.Format("startdate: {0:yyyy-MM-dd}, noof days: {1}, noof chars read: ", startDate, chunkSizeInDays));
                var jsonChunk = this.GetJson(jqlWithChunkFilter, AppConfigWrapper.MaxResult);
                Debug.WriteLine(jsonChunk.Length);
                result.Add(jsonChunk);
                startDate = startDate.Value.AddDays(chunkSizeInDays);
            }

            return result;
        }

        public DateTime? GetFirstIssuesStartDate(string jql)
        {
            var jqlOrdered = JqlChunkHelper.SetToOrderByCreadedDate(jql);
            var json = this.GetJson(jqlOrdered, 1);
            var createdDate = JqlChunkHelper.GetCreatedDates(json).FirstOrDefault();
            return createdDate;
        }

        private string GetJson(string jql, int maxResult)
        {
            var bytes = GetPostData(jql, maxResult);
            var request = SendRequest(bytes);
            var json = ReadResult(request);
            return json;
        }

        private static HttpWebRequest SendRequest(byte[] bytes)
        {
            var request = (HttpWebRequest)WebRequest.Create(AppConfigWrapper.JiraBaseUrl);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.Headers.Add("Authorization", "Basic " + GetEncodedCredentials());
            request.ContentLength = bytes.Length;

            using (var writer = request.GetRequestStream())
            {
                writer.Write(bytes, 0, bytes.Length);
            }

            return request;
        }

        private static string ReadResult(HttpWebRequest request)
        {
            string result;
            var response = request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        private static byte[] GetPostData(string jql, int maxResult)
        {
            var data = string.Format(@"{{""jql"":""{0}"",""maxResults"":{1},""expand"":[""changelog""]}}", jql, maxResult);
            var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
            return bytes;
        }

        private static string GetEncodedCredentials()
        {
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(AppConfigWrapper.JiraCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}