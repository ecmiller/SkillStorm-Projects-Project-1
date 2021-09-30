using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Data
{
    public class Flight
    {
        public int FlightID { get; set; }

        [Display(Name = "Airline")]
        public string Airline { get; set; }

        [Display(Name = "Departing From")]
        public string DepartureLocation { get; set; }

        [Display(Name = "Departure Time")]
        [DataType(DataType.DateTime)]
        public string DepartureTime { get; set; }

        [Display(Name = "Arriving At")]
        public string ArrivalLocation { get; set; }

        [Display(Name = "Arrival Time")]
        [DataType(DataType.DateTime)]
        public string ArrivalTime { get; set; }

        public List<Passenger> Passengers { get; set; }
        
        public Flight() { }

        public Flight(string airline, string departurelocation, string departureTime, string arrivalLocation,
            string arrivalTime)
        {
            this.Airline = airline;
            this.DepartureLocation = departurelocation;
            this.DepartureTime = departureTime;
            this.ArrivalLocation = arrivalLocation;
            this.ArrivalTime = arrivalTime;
        }
    }
}
