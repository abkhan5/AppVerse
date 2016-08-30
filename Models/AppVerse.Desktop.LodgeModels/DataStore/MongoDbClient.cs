using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AppVerse.Desktop.LodgeModels.DataStore
{
    public class MongoDbClient
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoDbClient()
        {


            _client = new MongoClient();
            _database = _client.GetDatabase("test");
        }

    }

}
