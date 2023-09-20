namespace SearchService.Entities
{
    public enum HotelClass
    {
        Business = 'D',
        Economy = 'S'
    }
    public class HotelDetails
    {
        public HotelDetails(string name, string owner, DateTime from, DateTime to, string city, Dictionary<string, string> price, Dictionary<string, int> availability)
        {   
            Name = name;
            Owner = owner;
            From = from;
            To = to;
            Price = price;
            Availability = availability;
            City = city;
        }
        public string Name { get; set; }
        public string Owner { get; set; } // ITC | Taj.
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string City { get; set; }
        public Dictionary<string, string> Price { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, int> Availability { get; set; } = new Dictionary<string, int>();
    }
}