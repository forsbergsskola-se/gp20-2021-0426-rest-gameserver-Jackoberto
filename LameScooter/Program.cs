using System;
using System.Threading.Tasks;
using LameScooter.RentalServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LameScooter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                }
            };
            IRental rental = args[1] switch
            {
                "offline" => new OfflineLameScooterRental(),
                "deprecated" => new DeprecatedLameScooterRental(),
                "realtime" => new RealTimeLameScooterRental(),
                _ => throw new ArgumentException($"{args[1]} is not a valid argument")
            };
            try
            {
                var amount = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"The Number Of Available Bikes At Station {args[0]} is {amount}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Invalid Argument: {e.Message}");
                throw;
            }
            catch (NotFoundException e)
            {
                Console.WriteLine($"Could not find: {e.Message}");
                throw;
            }
        }
    }
}
