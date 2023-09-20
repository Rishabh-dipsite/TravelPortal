using Microsoft.AspNetCore.Mvc;
using SearchService.Entities;
using SearchService.Seeding;
using System.Net;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        public List<FlightDetailSchema> cachedFlightData { get; set; }
        public List<HotelDetailSchema> cachedHotelData { get; set; }
        public SearchController(ILogger<SearchController> logger)
        {
            cachedFlightData = GenerateData.seedFlightData();
            cachedHotelData = GenerateData.seedHotelData();
            _logger = logger;
        }

        [HttpGet("flight/{from}/{to}/{flightClass}/{date}", Name = "GetFlights")]
        [ProducesResponseType(typeof(IEnumerable<FlightDetailSchema>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<FlightDetailSchema>>> GetFlights(string from, string to, string flightClass, string date)
        {
            DateTime departureDate;
            if (!DateTime.TryParse(date, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.AssumeUniversal, out departureDate))
                return BadRequest("Invalid date!");
            if (!EntityConstants.FlightClass.Any(c => c == flightClass)) return BadRequest("Invalid hotel class");

            return Ok(cachedFlightData.FindAll(flight =>
                        flight.From == from && 
                        flight.To == to && 
                        flight.Availability[flightClass] > 0 && 
                        flight.Schedule.Contains(departureDate.DayOfWeek.ToString().Substring(0, 2))
                    ));
        }


        [HttpGet("hotel/{from}/{to}/{hotelClass}/{city}/{name}", Name = "GetHotels")]
        [ProducesResponseType(typeof(IEnumerable<HotelDetailSchema>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<HotelDetailSchema>>> GetHotels(string from, string to, string hotelClass, string city, string name)
        {
            DateTime checkinDate, checkoutDate;
            if (!DateTime.TryParse(from, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.AssumeUniversal, out checkinDate) ||
                !DateTime.TryParse(to, new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.AssumeUniversal, out checkoutDate))
                return BadRequest("Invalid date!");
            if (!EntityConstants.HotelClass.Any(c => c == hotelClass)) return BadRequest("Invalid hotel class");
            return Ok(cachedHotelData.FindAll(hotel =>
                        hotel.From.CompareTo(checkinDate) < 0 &&
                        hotel.To.CompareTo(checkoutDate) > 0 &&
                        hotel.Availability.ContainsKey(hotelClass) && hotel.Availability[hotelClass] > 0 &&
                        hotel.City.ToLower() == city.ToLower() &&
                        hotel.Name.ToLower().Contains(name.ToLower())
                    ));
        }
    }
}