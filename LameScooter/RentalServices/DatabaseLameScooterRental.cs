using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace LameScooter.RentalServices
{
    public class DatabaseLameScooterRental : IRental
    {
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var databaseNames = await client.ListDatabaseNamesAsync();
            while (await databaseNames.MoveNextAsync())
            {
                foreach (var s in databaseNames.Current)
                {
                   Console.WriteLine(s); 
                }
            }

            throw new NotImplementedException();
        }
    }
}