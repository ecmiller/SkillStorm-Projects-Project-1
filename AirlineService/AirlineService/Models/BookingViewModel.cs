using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Models
{
    public class BookingViewModel
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }

        [Display(Name = "FlightID")]
        public int FlightID { get; set; }

        public override string ToString()
        {
            // Returns a string F(flight number)-P(passenger number)
            return $"Booking: {this.BookingID}";
        }
    }
}
