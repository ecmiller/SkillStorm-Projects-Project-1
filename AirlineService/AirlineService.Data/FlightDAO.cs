using System;
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
    }
}
