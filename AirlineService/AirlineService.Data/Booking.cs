using System;
namespace AirlineService.Data
{

    public class Booking
    {
        public int PassengerID { get; set; }
        public int FlightID { get; set; }
        public string BookingString { get; set; }

        public Booking() { }

        public Booking(int passengerID, int flightID)
        {
            this.PassengerID = passengerID;
            this.FlightID = flightID;
            this.BookingString = $"F{flightID}-P{passengerID}";
        }

        public override string ToString()
        {
            // Returns a string F(flight number)-P(passenger number)
            return this.BookingString;
        }
    }
}