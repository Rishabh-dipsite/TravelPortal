namespace SharedClassLibrary.Entities.Requests
{
    public enum EntityType
    {
        Flight,
        Hotel
    }
    public class CreateBookingRequest
    {
        public string EntityId { get; set; }
        public EntityType Type { get; set; }
        public string Provider { get; set; } // Indigo, Vistara | ITC, Taj
        public string Date { get; set; } // City in case of hotel booking
        public string From { get; set; }
        public string To { get; set; }
        public string Class { get; set; }
    }
}
