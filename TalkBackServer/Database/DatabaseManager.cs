using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Database
{
    class DatabaseManager
    {
        public static DatabaseManager Instance = new DatabaseManager();
        private MongoClient client;
        private IMongoDatabase database;

        public void InitDatabase()
        {//4TD0hcekGZOXfe0k
            client = new MongoClient
               (@"mongodb+srv://ofir:4TD0hcekGZOXfe0k@cluster0-qbfpc.mongodb.net/test?retryWrites=true&w=majority");
            database = client.GetDatabase("talkback");
            InitDBCollections();
        }
        public IMongoCollection<User> GetUserCollection()
        {
            var collection = database.GetCollection<User>("user");
            return collection;
        }
        public async Task<bool> RegisterUser(string name, string pass)
        {
            User tmp = await GetUserCollection().Find(x => x.name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
            if (tmp != null)
                return false;
            tmp = new User { name = name, pass = pass };
            GetUserCollection().InsertOne(tmp);
            return true;


        }
        private async void InitDBCollections()
        {
          await  GetUserCollection().Find(x => x.name.ToLower() == "-1").FirstOrDefaultAsync();
        }
        public async Task<bool> GetUser(string name, string pass)
        {
            User tmp = await GetUserCollection().Find(x => x.name.ToLower() == name.ToLower() && x.pass == pass).FirstOrDefaultAsync();
            if (tmp == null)
                return false;
            return true;


        }

    }
}
