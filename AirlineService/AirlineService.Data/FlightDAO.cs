using System;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString =
            @"Server=localhost, 1433;
            Database=master;
            User Id=sa;
            Password=Strong.Pwd-123";

        public FlightDAO() { }

        public void AddFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        public Flight GetFlight(int flightID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Flight> GetFlights()
        {
            throw new NotImplementedException();
        }
    }
}
