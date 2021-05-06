using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LameScooter.Data;
using Newtonsoft.Json;

namespace LameScooter
{
    public class OfflineLameScooterRental : IRental
    {
        private string FilePath => "scooters.json";

        public async Task<int> GetScooterCountInStation(string stationName)
        {
            var json = await File.ReadAllTextAsync(FilePath);
            var data = JsonConvert.DeserializeObject<Station[]>(json);
            var station = data.FirstOrDefault(station => station.Name == stationName);
            return station.BikesAvailable;
        }
    }
}