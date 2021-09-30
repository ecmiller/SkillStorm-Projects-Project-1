using System;
using System.Collections;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public interface IFlightDAO
    {
        public IEnumerable<Flight> GetFlights();
        public void AddFlight(Flight flight);
        public void GetFlight(int flightID);
    }
}
