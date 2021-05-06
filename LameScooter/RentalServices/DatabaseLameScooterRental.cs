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
                foreach (var bsonElement in s.Elements)
                {
                    if (bsonElement.Name == "bikesAvailable")
                        return bsonElement.Value.AsInt32;
                }
                // var bsonValue = s.AsBsonValue;
                // var json = bsonValue.ToJson();
                // var indexOf = json.IndexOf(',');
                // json = json.Remove(1, indexOf);
                // var obj = JsonConvert.DeserializeObject<Station>(json);
                // return obj.BikesAvailable;
            }
            
            throw new NotFoundException(stationName);
        }
    }
}