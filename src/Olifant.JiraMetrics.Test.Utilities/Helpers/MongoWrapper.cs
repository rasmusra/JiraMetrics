using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void Init(string connectionString, IList<Issue> createFromFiles)
        {
            _instance = new MongoWrapper(connectionString);
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

        public void InitTestPopulation(IList<Issue> issues)
        {
            DropDatabase();
            GetCollection<Issue>().InsertBatch(issues);
        }

        public MongoCollection<T> GetCollection<T>()
        {
            var client = new MongoClient(MongoConnectionStringBuilder.ConnectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(MongoConnectionStringBuilder.DatabaseName);
            return db.GetCollection<T>(typeof(T).Name.ToLower());
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
