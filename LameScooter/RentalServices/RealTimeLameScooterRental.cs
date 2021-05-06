using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LameScooter.Data;
using Newtonsoft.Json;

namespace LameScooter.RentalServices
{
    public class RealTimeLameScooterRental : IRental
    {
        private const string Url =
            "https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json";
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            if (stationName.Any(char.IsNumber))
                throw new ArgumentException($"{stationName} Contains Numbers This Is Not Allowed");
            var data = await RequestData();
            var stationContainer = JsonConvert.DeserializeObject<StationContainer>(data);
            var station = stationContainer.Stations.FirstOrDefault(station => station.Name == stationName);
            if (station == null)
                throw new NotFoundException(stationName);
            return station.BikesAvailable;
        }

        private async Task<string> RequestData()
        {
            var httpClient = new HttpClient();
            return await httpClient.GetStringAsync(Url);
        }
    }
}