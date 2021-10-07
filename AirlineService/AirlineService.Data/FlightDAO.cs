using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineService.Data
{
    public class FlightDAO : IFlightDAO
    {
        private string connString =
            @"Server=localhost, 1433;
            Database=master;
            User Id=sa;
            Password=Strong.Pwd-123";

        public IEnumerable<Flight> GetFlights()
        {
            List<Flight> FlightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM airline.Flights", conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Flight temp = new Flight();
                    while (reader.Read())
                    {
                        temp.FlightID = Convert.ToInt32(reader["FlightID"]);
                        temp.Airline = reader["Airline"].ToString();
                        temp.DepartureLocation = reader["DepartureLocation"].ToString();
                        temp.DepartureTime = DateTime.Parse(reader["DepartureTime"].ToString());
                        temp.ArrivalLocation = reader["ArrivalLocation"].ToString();
                        temp.ArrivalTime = DateTime.Parse(reader["ArrivalTime"].ToString());
                        temp.SeatsRemaining = Convert.ToInt32(reader["SeatsRemaining"]);
                        temp.MaxCapacity = Convert.ToInt32(reader["MaxCapacity"]);

                        Console.WriteLine("Adding flight in the GetFlights method -- " + temp.ToString());
                        FlightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all the Flights!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

            return FlightList;
        }

        public Flight GetFlight(int id)
        {
            Flight flight = new Flight();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "SELECT * FROM airline.Flights WHERE FlightID = @ID";

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
                        flight.FlightID = Convert.ToInt32(reader["FlightID"]);
                        flight.Airline = reader["Airline"].ToString();
                        flight.DepartureLocation = reader["DepartureLocation"].ToString();
                        flight.DepartureTime = Convert.ToDateTime(reader["DepartureTime"]);
                        flight.ArrivalLocation = reader["ArrivalLocation"].ToString();
                        flight.ArrivalTime = Convert.ToDateTime(reader["ArrivalTime"]);
                        flight.SeatsRemaining = Convert.ToInt32(reader["SeatsRemaining"]);
                        flight.MaxCapacity = Convert.ToInt32(reader["MaxCapacity"]);
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to get flight {0} from the database.\n{1}", id, ex.Message);
                }
            }

            Console.WriteLine(flight.ToString());
            Console.WriteLine("GetFlight method end");

            return flight;
        }

        public void AddFlight(Flight flight)
        {
            int id = 0;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[airline].[AddFlight]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Airline", flight.Airline);
                cmd.Parameters.AddWithValue("@DepartureLocation", flight.DepartureLocation);
                cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                cmd.Parameters.AddWithValue("@ArrivalLocation", flight.ArrivalLocation);
                cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
                cmd.Parameters.AddWithValue("@SeatsRemaining", flight.SeatsRemaining);
                cmd.Parameters.AddWithValue(@"MaxCapacity", flight.MaxCapacity);
                cmd.Parameters.Add("@outID", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@outID"].Value;
                    flight.FlightID = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void RemoveFlight(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "DELETE * FROM airline.Flights WHERE FlightID = @ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                try
                {
                    conn.Open();
                    Console.WriteLine("Connecting to the airline database");

                    cmd.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Failed to remove flight {0} from the database.\n{1}", id, ex.Message);
                }
                Console.WriteLine("RemoveFlight method end");
            }
        }

        public DataTable GetTable()
        {
            DataTable dt = new DataTable();
            string query = "Select * from airline.Flights";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    conn.Open();

                    adapter.Fill(dt);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error could not fill the data table!\n{0}", ex.Message);
                }
            }

            return dt;
        }

        public void UpdateFlight(Flight flight)
        {

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = "[airline].[UpdateFlight]";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", flight.FlightID);
                cmd.Parameters.AddWithValue("@Airline", flight.Airline);
                cmd.Parameters.AddWithValue("@DepartureLocation", flight.DepartureLocation);
                cmd.Parameters.AddWithValue("@DepartureTime", flight.DepartureTime);
                cmd.Parameters.AddWithValue("@ArrivalLocation", flight.ArrivalLocation);
                cmd.Parameters.AddWithValue("@ArrivalTime", flight.ArrivalTime);
                cmd.Parameters.AddWithValue("@SeatsRemaining", flight.SeatsRemaining);
                cmd.Parameters.AddWithValue(@"MaxCapacity", flight.MaxCapacity);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update flight!\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
