using System;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> GetPassengers();
        public void AddPassenger(Passenger passenger);
        public Passenger GetPassenger(int passengerID);
    }
}
