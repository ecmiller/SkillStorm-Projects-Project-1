using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Models
{
    public class BookingViewModel
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Required]
        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }

        [Required]
        [Display(Name = "Flight ID")]
        public int FlightID { get; set; }

        [Display(Name = "Confirmation Number")]
        public string ConfirmationNumber { get; set; }

        public override string ToString()
        {
            // Returns a string F(flight number)-P(passenger number)
            return $"Booking: {this.BookingID}";
        }
    }
}
