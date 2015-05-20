using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Olifant.JiraMetrics.Lib.Jira.Model;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public class MongoWrapper
    {
        private MongoConnectionStringBuilder MongoConnectionStringBuilder { get; set; }
        private static MongoWrapper _instance;

        private MongoWrapper(string connectionString)
        {
            MongoConnectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
        }

        public static void Init(string connectionString, IList<Issue> issues, IList<string> jiraStubs)
        {
            _instance = new MongoWrapper(connectionString);
            _instance.InitTestPopulation(issues, jiraStubs);
        }

        public static MongoWrapper Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("Need to init this singleton first");
                }
                return _instance;
            }
        }

        public void InitTestPopulation(IList<Issue> issues, IList<string> jiraJson)
        {
            DropDatabase();
            GetCollection<Issue>().InsertBatch(issues);

            var jiraBson = jiraJson.Select(BsonSerializer.Deserialize<BsonDocument>);
            GetDb().GetCollection("jira").InsertBatch(jiraBson);
        }

        public MongoCollection<T> GetCollection<T>()
        {
            return GetDb().GetCollection<T>(typeof(T).Name.ToLower());
        }

        private MongoDatabase GetDb()
        {
            var client = new MongoClient(MongoConnectionStringBuilder.ConnectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(MongoConnectionStringBuilder.DatabaseName);
            return db;
        }

        private void DropDatabase()
        { 
            var client = new MongoClient(MongoConnectionStringBuilder.ConnectionString);
            var server = client.GetServer();

            if (server.DatabaseExists(MongoConnectionStringBuilder.DatabaseName))
            {
                server.DropDatabase(MongoConnectionStringBuilder.DatabaseName);
            }
        }
    }
}
