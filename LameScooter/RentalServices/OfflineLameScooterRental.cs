using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LameScooter.Data;
using Newtonsoft.Json;

namespace LameScooter.RentalServices
{
    public class OfflineLameScooterRental : IRental
    {
        private string FilePath => "scooters.json";

        public async Task<int> GetScooterCountInStation(string stationName)
        {
            if (stationName.Any(char.IsNumber))
                throw new ArgumentException($"{stationName} Contains Numbers This Is Not Allowed");
            var data = await File.ReadAllTextAsync(FilePath);
            var stations = JsonConvert.DeserializeObject<Station[]>(data);
            var station = stations.FirstOrDefault(station => station.Name == stationName);
            if (station == null)
                throw new NotFoundException(stationName);
            return station.BikesAvailable;
        }
    }
}