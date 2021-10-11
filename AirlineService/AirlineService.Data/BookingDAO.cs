using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AirlineService.Data
{
    public class BookingDAO : IBookingDAO
    {
        private string connString =
            @"Server=localhost, 1433;
            Database=master;
            User Id=sa;
            Password=Strong.Pwd-123";

        public BookingDAO() { }

        public void BookFlight(int passID, int flightID)
        {
            FlightDAO flightDAO = new FlightDAO();
            PassengerDAO passengerDAO = new PassengerDAO();

            Flight flight = flightDAO.GetFlight(flightID);
            Passenger passenger = passengerDAO.GetPassenger(passID);

            if (flight.SeatsRemaining > 0)
            {
                Booking booking = new Booking(passID, flightID);
                passenger.Bookings.Add(booking.ConfirmationNumber);
                flight.SeatsRemaining--;

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "[airline].[AddBooking]";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PassengerID", passID);
                    cmd.Parameters.AddWithValue("@FlightID", flightID);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Could not add booking!\n{0}", ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            else
            {
                Console.WriteLine("There were no seats avaialble on flight " + flightID);
            }

            Console.WriteLine($"Booked passenger {passID} on flight {flightID}");
        }

        public void RemoveBooking(int bookingID)
        {
            PassengerDAO passengerDAO = new PassengerDAO();
            FlightDAO flightDAO = new FlightDAO();
            Booking booking = GetBooking(bookingID);

            // Remove the confirmation number from the passenger's booking list
            Passenger passenger = passengerDAO.GetPassenger(booking.PassengerID);
            passenger.Bookings.Remove(booking.ConfirmationNumber);
            passengerDAO.UpdatePassenger(passenger);

            // One seat is now free, so we increment the remaining seats by 1
            Flight flight = flightDAO.GetFlight(booking.FlightID);
            if (flight.SeatsRemaining < flight.MaxCapacity)
            {
                flight.SeatsRemaining++;
                flightDAO.UpdateFlight(flight);
            }
            else
            {
                Console.WriteLine("Seats available already met or exceeded max capacity");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM airline.Bookings WHERE BookingID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", bookingID);

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to remove booking {0} from the database.\n{1}", bookingID, ex.Message);
                }
                Console.WriteLine("RemoveBooking method end");
            }
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            List<Booking> BookingsList = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM airline.Bookings", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking temp = new Booking();
                        temp.BookingID = Convert.ToInt32(reader["BookingID"]);
                        temp.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                        temp.FlightID = Convert.ToInt32(reader["FlightID"]);

                        // --- TESTING ---
                        Console.WriteLine("Got this Booking from the database -- " + temp.ToString());
                        BookingsList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all the Bookings!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            // --- TESTING ---
            Console.WriteLine("Final Booking list:");
            foreach (Booking b in BookingsList)
            {
                Console.WriteLine(b.ToString());
            }

            return BookingsList;
        }

        public Booking GetBooking(int bookingID)
        {
            Booking booking = new Booking();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM airline.Bookings WHERE BookingID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                cmd.Parameters["@ID"].Value = bookingID;

                try
                {
                    conn.Open();
                    Console.WriteLine("Connecting to the airline database");

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        booking.BookingID = Convert.ToInt32(reader["BookingID"]);
                        booking.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                        booking.FlightID = Convert.ToInt32(reader["BookingID"]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to get booking {0} from the database.\n{1}", bookingID, ex.Message);
                }
            }
            return booking;
        }

        public IEnumerable<Booking> GetBookingsForPassenger(int passengerID)
        {
            List<Booking> BookingsList = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM airline.Bookings WHERE PassengerID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                cmd.Parameters["@ID"].Value = passengerID;

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking temp = new Booking();
                        temp.BookingID = Convert.ToInt32(reader["BookingID"]);
                        temp.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                        temp.FlightID = Convert.ToInt32(reader["FlightID"]);

                        // --- TESTING ---
                        Console.WriteLine("Got this Booking from the database -- " + temp.ToString());
                        BookingsList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all the Bookings!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return BookingsList;
        }
    }
}
