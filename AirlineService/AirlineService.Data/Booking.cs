using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Data
{

    public class Booking
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }

        [Display(Name = "Flight ID")]
        public int FlightID { get; set; }

        [Display(Name = "Confirmation Number")]
        public string ConfirmationNumber { get; set; }

        public Booking() { }

        public Booking(int passengerID, int flightID)
        {
            this.PassengerID = passengerID;
            this.FlightID = flightID;
            this.ConfirmationNumber = $"P{this.PassengerID}-F{this.FlightID}";
        }

        public override string ToString()
        {
            // Returns a string F(flight number)-P(passenger number)
            return $"Booking: {this.BookingID}";
        }
    }
}