using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LameScooter.Data;

namespace LameScooter.RentalServices
{
    public class DeprecatedLameScooterRental : IRental
    {
        private string FilePath => "scooters.txt";
        public async Task<int> GetScooterCountInStation(string stationName)
        {
            if (stationName.Any(char.IsNumber))
                throw new ArgumentException($"{stationName} Contains Numbers This Is Not Allowed");
            var text = await File.ReadAllTextAsync(FilePath);
            var stations = GetStations(text);
            var station = stations.FirstOrDefault(station => station.Name == stationName);
            if (station == null)
                throw new NotFoundException(stationName);
            return station.BikesAvailable;
        }

        private IEnumerable<IStation> GetStations(string textFile)
        {
            textFile = textFile.Trim('\n');
            var splits = textFile.Split('\n');
            foreach (var split in splits)
            {
                yield return GetStation(split);
            }
        }

        private IStation GetStation(string text)
        {
            var splits = text.Split(':', StringSplitOptions.TrimEntries);
            var name = splits[0];
            var bikesAvailable = int.Parse(splits[1]);
            return new Station()
            {
                Name = name,
                BikesAvailable = bikesAvailable
            };
        }
    }
}