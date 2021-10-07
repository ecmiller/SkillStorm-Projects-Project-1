using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Models
{
    public class FlightViewModel
    {
        public int FlightID { get; set; }

        [Required]
        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Required]
        [Display(Name = "Departure Location")]
        public string DepartureLocation { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required]
        [Display(Name = "Arrival Location")]
        public string ArrivalLocation { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Required]
        [Display(Name = "Seats Remaining")]
        public int SeatsRemaining { get; set; }

        [Required]
        [Display(Name = "Max Capacity")]
        public int MaxCapacity { get; set; }

        public override string ToString()
        {
            return $"FlightID: {this.FlightID} Departing: {this.DepartureLocation} {this.DepartureTime} Arriving: {this.ArrivalLocation} {this.ArrivalTime} Seats: {this.SeatsRemaining} / {this.MaxCapacity}";
        }
    }
}
