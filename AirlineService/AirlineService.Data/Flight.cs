using System;
namespace AirlineService.Data
{
    public class Flight
    {
        public int PassengerID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        // Booking
        public Flight() { }
    }
}
