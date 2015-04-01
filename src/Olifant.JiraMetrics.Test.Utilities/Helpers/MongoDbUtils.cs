using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MongoDB.Driver;

namespace Olifant.JiraMetrics.Test.Utilities.Helpers
{
    public class MongoHelper<T> where T : class 
    {
        public MongoCollection<T> Collection { get; private set; }
        
        public MongoHelper()
        {
            var con = new MongoConnectionStringBuilder("server=localhost:27113;database=JirametricsDb");
            var client = new MongoClient(con.ConnectionString);
            var server = client.GetServer();
            var db = server.GetDatabase(con.DatabaseName);
            Collection = db.GetCollection<T>(typeof(T).Name.ToLower());
        }
    }
}