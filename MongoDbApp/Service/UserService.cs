using MongoDB.Driver;
using MongoDbApp.Model;
using MongoDbApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Service
{

    public class UserService
    {
        private IDbSettings _settings;
        private IMongoCollection<User> _user;

        public UserService(IDbSettings settings)
        {
            _settings = settings;
            MongoClient client = new MongoClient(settings.ConnectionString);
            var db = client.GetDatabase(settings.Database);
            _user = db.GetCollection<User>(settings.Collection);
        }


        //Tümünü Listeleme
        public List<User> GetAll()
        {
            return _user.Find(u => true).ToList();
        }

        //Tek bir kullanıcı getirme
        public User GetSingle(string id)
        {
            return _user.Find(u => u.Id == id).FirstOrDefault();
        }

        //Kullanıcı ekleme
        public User Add(User user)
        {
            _user.InsertOne(user);
            return user;
        }

        //Kullanıcı güncelleme
        public long Update(User currentUser)
        {
            return _user.ReplaceOne(u => u.Id == currentUser.Id, currentUser).ModifiedCount;
        }

        //Kullanıcı silme
        public long Delete(string id)
        {
            return _user.DeleteOne(u => u.Id == id).DeletedCount;
        }

    }


}