using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSquareMaster
{
    public class WorldDao
    {
        private IMongoDatabase database;
        private IMongoClient client;
        private IMongoCollection<World> WorldCollection;
        private string connectionString;

        public WorldDao(string connectionString)
        {
            this.connectionString = connectionString;
            client = new MongoClient(connectionString);
            database = client.GetDatabase("World");
            WorldCollection = database.GetCollection<World>("World");
        }
        public void SaveWorld(World world)
        {
            WorldCollection.InsertOne(world);
        }

        public List<World> GetAllWorlds()
        {
            return WorldCollection.Find(_ => true).ToList();
        }
        public World GetWorldByName(string name)
        {
            return WorldCollection.Find(w => w.Name == name).FirstOrDefault();
        }
    }
}
