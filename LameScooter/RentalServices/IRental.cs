using System.Threading.Tasks;

namespace LameScooter.RentalServices
{
    public interface IRental
    {
        Task<int> GetScooterCountInStation(string stationName);
    }
}