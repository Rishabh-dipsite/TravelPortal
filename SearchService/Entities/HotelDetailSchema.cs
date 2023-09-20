namespace SearchService.Entities
{
    public class HotelDetailSchema : HotelDetails
    {
        public DateTime lastFetchTime { get; set; } // We will keep prefetched data in our network to provide better availability. It will be refreshed if TTL expires or another fetch is made.
        public HotelDetailSchema(string name, string owner, DateTime from, DateTime to, string city, Dictionary<string, string> price, Dictionary<string, int> availability) 
            : base(name, owner, from, to, city, price, availability)
        {
            lastFetchTime = DateTime.Now;
        }
    }
}
