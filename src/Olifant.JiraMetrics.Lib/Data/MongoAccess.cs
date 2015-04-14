using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Olifant.JiraMetrics.Lib.Data
{
    public class MongoAccess 
    {
        private MongoConnectionStringBuilder MongoConnectionStringBuilder { get; set; }

        public MongoAccess(string connectionString)
        {
            MongoConnectionStringBuilder = new MongoConnectionStringBuilder(connectionString);
        }

        public MongoCollection<T> GetCollection<T>()
        {
            var client = new MongoClient(MongoConnectionStringBuilder.ConnectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(MongoConnectionStringBuilder.DatabaseName);
            return db.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}
