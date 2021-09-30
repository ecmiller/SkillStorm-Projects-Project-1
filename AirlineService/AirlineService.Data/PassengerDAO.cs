using System;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public class PassengerDAO : IPassengerDAO
    {
        private string connString =
            @"Server=localhost, 1433;
            Database=master;
            User Id=sa;
            Password=Strong.Pwd-123";

        public PassengerDAO() { }

        void IPassengerDAO.AddPassenger(Passenger passenger)
        {
            throw new NotImplementedException();
        }

        Passenger IPassengerDAO.GetPassenger(int passengerID)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Passenger> IPassengerDAO.GetPassengers()
        {
            throw new NotImplementedException();
        }
    }
}
