namespace SearchService.Entities
{
    public enum FlightClass
    {
        Business = 'B',
        Economy = 'E'
    }
    public class FlightDetails
    {
        public FlightDetails(string name, string airline, string from, string to, Dictionary<string, string> price, Dictionary<string, int> availability, List<string> schedule)
        {   
            Name = name;
            Airline = airline;
            From = from;
            To = to;
            Price = price;
            Availability = availability;
            Schedule = schedule;
        }
        public string Name { get; set; }
        public string Airline { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Dictionary<string, string> Price { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, int> Availability { get; set; } = new Dictionary<string, int>();
        public List<string> Schedule { get; set; }  
    }
}