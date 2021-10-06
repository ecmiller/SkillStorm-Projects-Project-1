using System;
using System.Collections;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> GetFlights();
        public void AddFlight(Flight flight);
        public Flight GetFlight(int flightID);
        public void UpdateFlight(Flight flight);
        public void RemoveFlight(int flightID);
    }
}
