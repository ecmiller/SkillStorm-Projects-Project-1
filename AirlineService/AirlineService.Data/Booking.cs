﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AirlineService.Data
{

    public class Booking
    {
        [Display(Name = "Booking ID")]
        public int BookingID { get; set; }

        [Display(Name = "Passenger ID")]
        public int PassengerID { get; set; }

        [Display(Name = "FlightID")]
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