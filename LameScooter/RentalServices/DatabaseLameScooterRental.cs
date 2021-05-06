using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LameScooter.RentalServices
{
    public class DatabaseLameScooterRental : IRental
    {
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            if (stationName.Any(char.IsNumber))
                throw new ArgumentException($"{stationName} Contains Numbers This Is Not Allowed");
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("scooters");
            var collection = database.GetCollection<BsonDocument>("stations");
            var fieldDef = new StringFieldDefinition<BsonDocument, string>("name");
            var filter = Builders<BsonDocument>.Filter.Eq(fieldDef, stationName);
            var objects = await collection.Find(filter).ToListAsync();

            foreach (var s in objects)
            {
                if (s.TryGetValue("bikesAvailable", out var value))
                {
                    return value.AsInt32;
                }
            }
            
            throw new NotFoundException(stationName);
        }
    }
}