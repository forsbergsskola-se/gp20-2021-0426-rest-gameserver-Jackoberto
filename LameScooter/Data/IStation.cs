namespace LameScooter.Data
{
    public interface IStation
    {
        public string Name { get; set; }
        public int BikesAvailable { get; set; }
    }
}