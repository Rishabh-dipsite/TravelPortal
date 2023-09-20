using SearchService.Entities;

namespace SearchService.Seeding
{
    public static class GenerateData
    {
        public static List<FlightDetailSchema> seedFlightData()
        {
            return new List<FlightDetailSchema> {
                new FlightDetailSchema("Boeing-11", "Indigo", "Mumbai", "Delhi", new Dictionary<string, string> { { "B", "14000" }, { "E", "11000" } }, new Dictionary<string, int>{{ "B", 10}, { "E", 80} }, new List<string>{"Mo", "We", "Fr"}),
                new FlightDetailSchema("Jet-41", "SuperJet", "Mumbai", "Delhi", new Dictionary<string, string> { { "B", "28000" }, { "E", "22000" } }, new Dictionary<string, int>{{ "B", 2}, { "E", 8} }, new List<string>{"Tu","Th","Sa"}),
                new FlightDetailSchema("Jet-23", "Vistara", "Lucknow", "Delhi", new Dictionary<string, string> { { "B", "8000" }, { "E", "4000" } }, new Dictionary<string, int>{{ "B", 5}, { "E", 20} }, new List<string>{"Tu","Th","Sa" }),
                new FlightDetailSchema("Boeing-21", "Vistara", "Kolkata", "Delhi", new Dictionary<string, string> { { "B", "12000" }, { "E", "8000" } }, new Dictionary<string, int>{{ "B", 7}, { "E", 70} }, new List<string>{"Su" })
            };

        }

        public static List<HotelDetailSchema> seedHotelData()
        {
            return new List<HotelDetailSchema> {
                new HotelDetailSchema("Lemon Tree Premier 1", "Lemon Tree", new DateTime(2020, 01, 01), new DateTime(2024, 01, 01), "Delhi", new Dictionary<string, string> { { "SD", "14000" }, { "D", "11000" } }, new Dictionary<string, int>{{"SD",10}, { "D", 80} }),
                new HotelDetailSchema("Taj Deluxe", "Taj", new DateTime(2020, 01, 01), new DateTime(2024, 01, 01), "Gugaon", new Dictionary<string, string> { { "SD", "12000" }, { "D", "9000" } }, new Dictionary<string, int>{{"SD",8}, { "D", 6} }),
                new HotelDetailSchema("Renaisance", "ITC", new DateTime(2020, 01, 01), new DateTime(2024, 01, 01), "Noida", new Dictionary<string, string> { { "SD", "11000" }, { "D", "10000" } }, new Dictionary<string, int>{{"SD",2}, { "D", 20} }),
                new HotelDetailSchema("Lemon Tree Premier 2", "Lemon Tree", new DateTime(2020, 01, 01), new DateTime(2024, 01, 01), "Delhi", new Dictionary<string, string> { { "SD", "15000" }, { "D", "12000" } }, new Dictionary<string, int>{{"SD",5}, { "D", 10} })
            };

        }
    }
}
