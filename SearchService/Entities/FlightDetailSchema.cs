namespace SearchService.Entities
{
    public class FlightDetailSchema : FlightDetails
    {
        public DateTime lastFetchTime{ get; set; } // We will keep prefetched data in our network to provide better availability. It will be refreshed if TTL expires or another fetch is made.
        public FlightDetailSchema(string name, string airline, string from, string to, Dictionary<string, string> price, Dictionary<string, int> availability, List<string> schedule) 
            : base(name, airline, from, to, price, availability, schedule)
        {
            lastFetchTime = DateTime.Now;
        }
    }
}
