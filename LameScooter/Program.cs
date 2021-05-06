using System;
using System.Threading.Tasks;
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
            IRental rental = new OfflineLameScooterRental();
            var amount = await rental.GetScooterCountInStation(args[0]);
            Console.WriteLine($"The Number Of Available Bikes At Station {args[0]} is {amount}");
        }
    }
}
