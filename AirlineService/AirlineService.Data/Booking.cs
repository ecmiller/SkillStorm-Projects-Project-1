using System;
namespace AirlineService.Data
{

    public class Booking
    {
        public int BookingID { get; set; }
        public int PassengerID { get; set; }
        public int FlightID { get; set; }

        public Booking() { }

        public Booking(int passengerID, int flightID)
        {
            this.PassengerID = passengerID;
            this.FlightID = flightID;
        }

        public override string ToString()
        {
            // Returns a string F(flight number)-P(passenger number)
            return $"Booking: {this.BookingID}";
        }
    }
}