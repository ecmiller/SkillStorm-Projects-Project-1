using System;
using System.Collections.Generic;

namespace AirlineService.Data
{
    public interface IBookingDAO
    {
        public IEnumerable<Booking> GetAllBookings();
        public Booking GetBooking(int id);
        public IEnumerable<Booking> GetBookingsForPassenger(int id);
        public void BookFlight(int passID, int flightID);
        public void RemoveBooking(int bookingID);
    }
}