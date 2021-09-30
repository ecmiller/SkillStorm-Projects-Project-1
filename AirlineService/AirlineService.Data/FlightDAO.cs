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

        void IFlightDAO.AddFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        void IFlightDAO.GetFlight(int flightID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Flight> IFlightDAO.GetFlights()
        {
            throw new NotImplementedException();
        }
    }
}
