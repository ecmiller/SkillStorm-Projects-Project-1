using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

        public Passenger GetPassenger(int id)
        {
            Passenger passenger = new Passenger();
            BookingDAO bookingDAO = new BookingDAO();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM airline.Passengers WHERE PassengerID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@ID", System.Data.SqlDbType.Int);
                cmd.Parameters["@ID"].Value = id;

                try
                {
                    conn.Open();
                    Console.WriteLine("Connecting to the airline database");

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        passenger.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                        passenger.Name = reader["Name"].ToString();
                        passenger.Age = Convert.ToInt32(reader["Age"]);
                        passenger.Email = reader["Email"].ToString();
                        passenger.Bookings = new List<string>();

                        foreach(Booking b in bookingDAO.GetBookingsForPassenger(id))
                        {
                            passenger.Bookings.Add(b.ConfirmationNumber);
                        }
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to get passenger {0} from the database.\n{1}", id, ex.Message);
                }
            }

            // --- Testing ---
            Console.WriteLine("Got passenger -- " + passenger.ToString());
            Console.WriteLine("GetPassenger method end");

            return passenger;
        }

        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> PassengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM airline.Passengers", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Passenger temp = new Passenger();
                        temp.PassengerID = Convert.ToInt32(reader["PassengerID"]);
                        temp.Name = reader["Name"].ToString();
                        temp.Age = Convert.ToInt32(reader["Age"]);
                        temp.Email = reader["Email"].ToString();
                        // temp.Bookings = GetBookings(temp.PassengerID);

                        // --- TESTING ---
                        Console.WriteLine("Got this Passenger from the database -- " + temp.ToString());
                        PassengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all the Passengers!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            // --- TESTING ---
            Console.WriteLine("Final Passenger list:");
            foreach (Passenger p in PassengerList)
            {
                Console.WriteLine(p.ToString());
            }

            return PassengerList;
        }


        public void AddPassenger(Passenger passenger)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[airline].[AddPassenger]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", passenger.Name);
                cmd.Parameters.AddWithValue("@Age", passenger.Age);
                cmd.Parameters.AddWithValue("@Email", passenger.Email);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void RemovePassenger(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE FROM airline.Passengers WHERE PassengerID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to remove passenger {0} from the database.\n{1}", id, ex.Message);
                }
                Console.WriteLine("RemovePassenger method end");
            }
        }

        public void UpdatePassenger(Passenger passenger)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[airline].[UpdatePassenger]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", passenger.PassengerID);
                cmd.Parameters.AddWithValue("@Name", passenger.Name);
                cmd.Parameters.AddWithValue("@Age", passenger.Age);
                cmd.Parameters.AddWithValue("@Email", passenger.Email);
                Console.WriteLine("Updating with passenger info--" + passenger.ToString());
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update passenger!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Booking> GetBookings(int id)
        {
            List<Booking> bookings = new List<Booking>();
            IEnumerable<Booking> temp = new BookingDAO().GetBookingsForPassenger(id);

            foreach(Booking b in temp)
            {
                bookings.Add(b);
            }

            return bookings;
        }
    }
}
