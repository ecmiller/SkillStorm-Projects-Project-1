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
        public DateTime DepartureTime { get; set; }

        [Display(Name = "Arriving At")]
        public string ArrivalLocation { get; set; }

        [Display(Name = "Arrival Time")]
        [DataType(DataType.DateTime)]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Seats Remaining")]
        public int SeatsRemaining { get; set; }

        [Display(Name = "Max Capacity")]
        public int MaxCapacity { get; set; }

        public Flight() { }

        public Flight(string airline, string departurelocation, DateTime departureTime, string arrivalLocation,
            DateTime arrivalTime, int seatsRemaining, int maxCapacity)
        {
            this.Airline = airline;
            this.DepartureLocation = departurelocation;
            this.DepartureTime = departureTime;
            this.ArrivalLocation = arrivalLocation;
            this.ArrivalTime = arrivalTime;
            this.SeatsRemaining = seatsRemaining;
            this.MaxCapacity = maxCapacity;
        }

        public Flight(int flightID, string airline, string departurelocation, DateTime departureTime, string arrivalLocation,
            DateTime arrivalTime, int seatsRemaining, int maxCapacity)
        {
            this.FlightID = flightID;
            this.Airline = airline;
            this.DepartureLocation = departurelocation;
            this.DepartureTime = departureTime;
            this.ArrivalLocation = arrivalLocation;
            this.ArrivalTime = arrivalTime;
            this.SeatsRemaining = seatsRemaining;
            this.MaxCapacity = maxCapacity;
        }

        public override string ToString()
        {
            return $"FlightID: {this.FlightID} Departing: {this.DepartureLocation} {this.DepartureTime} Arriving: {this.ArrivalLocation} {this.ArrivalTime} Seats: {this.SeatsRemaining} / {this.MaxCapacity}";
        }
    }
}
