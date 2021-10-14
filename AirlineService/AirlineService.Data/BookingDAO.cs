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

        public void BookFlight(int passengerID, int flightID)
        {
            Booking booking = new Booking(passengerID, flightID);

            // Testing
            Console.WriteLine("Trying to book: " + booking.ToString());

            FlightDAO flightDAO = new FlightDAO();
            PassengerDAO passengerDAO = new PassengerDAO();

            Flight flight = flightDAO.GetFlight(flightID);
            Passenger passenger = passengerDAO.GetPassenger(passengerID);

            if (flight.SeatsRemaining > 0)
            {
                // Update the list of bookings for the passenger added to the flight
                passenger.Bookings.Add(booking.ConfirmationNumber);

                // Update the number of avaialbe seats left on the plane
                flight.SeatsRemaining--;
                flightDAO.UpdateFlight(flight);

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    string query = "[airline].[AddBooking]";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PassengerID", booking.PassengerID);
                    cmd.Parameters.AddWithValue("@FlightID", booking.FlightID);
                    cmd.Parameters.AddWithValue("@ConfirmationNumber", booking.ConfirmationNumber);

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
                Console.WriteLine("There were no seats avaialble on flight " + flight.FlightID);
            }

            Console.WriteLine($"Booked passenger {booking.PassengerID} on flight {booking.FlightID}");
        }

        public void RemoveBooking(Booking booking)
        {
            // Remove the confirmation number from the passenger's booking list
            PassengerDAO passengerDAO = new PassengerDAO();
            Passenger passenger = passengerDAO.GetPassenger(booking.PassengerID);
            passenger.Bookings.Remove(booking.ConfirmationNumber);
            passengerDAO.UpdatePassenger(passenger);

            // One seat is now free, so we increment the remaining seats by 1
            FlightDAO flightDAO = new FlightDAO();
            Flight flight = flightDAO.GetFlight(booking.FlightID);

            // Testing
            // Console.WriteLine("Flight before attempting to remove booking:\n" + flight.ToString());

            if (flight.SeatsRemaining < flight.MaxCapacity)
            {
                flight.SeatsRemaining++;
                flightDAO.UpdateFlight(flight);

                // Console.WriteLine("Flight after attempting to remove booking:\n" + flight.ToString());
            }
            else
            {
                Console.WriteLine("Seats available already met or exceeded max capacity");
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM airline.Bookings WHERE BookingID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", booking.BookingID);

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to remove booking {0} from the database.\n{1}", booking.BookingID, ex.Message);
                }
                Console.WriteLine("RemoveBooking method end");
            }
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            List<Booking> BookingsList = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM airline.Bookings  ORDER BY BookingID", conn);

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
                        temp.ConfirmationNumber = reader["ConfirmationNumber"].ToString();

                        // --- TESTING ---
                        // Console.WriteLine("Got this Booking from the database -- " + temp.ToString());
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
            //Console.WriteLine("Final Booking list:");
            //foreach (Booking b in BookingsList)
            //{
            //    Console.WriteLine(b.ToString());
            //}

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
                        booking.FlightID = Convert.ToInt32(reader["FlightID"]);
                        booking.ConfirmationNumber = reader["ConfirmationNumber"].ToString();
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
                        temp.ConfirmationNumber = reader["ConfirmationNumber"].ToString();

                        // --- TESTING ---
                        // Console.WriteLine("Got this Booking from the database -- " + temp.ToString());
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
