using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class DatabaseServer
    {
        private readonly IMongoCollection<Map> _mapCollection;
        private string connectionString;
        private IMongoClient client;

        public DatabaseServer(string connectionString)
        {
            this.connectionString = connectionString;
            client = new MongoClient(connectionString);
            var database = client.GetDatabase("MapDatabase");
            _mapCollection = database.GetCollection<Map>("Maps");
        }

        public void InsertMap(Map map)
        {
            _mapCollection.InsertOne(map);
        }
    }
}
