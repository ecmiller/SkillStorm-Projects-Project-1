using System;
using System.Collections.Generic;
using System.Data;

namespace AirlineService.Data
{
    public interface IPassengerDAO
    {
        public IEnumerable<Passenger> GetPassengers();
        public void AddPassenger(Passenger passenger);
        public Passenger GetPassenger(int passengerID);
        public void UpdatePassenger(Passenger passenger);
        public void RemovePassenger(int id);
        public SortedSet<Booking> GetBookings(int id);
    }
}
