using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AirlineService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            FlightDAO fl = new FlightDAO();
            List<Flight> flights = new List<Flight>(fl.GetFlights());

            foreach (Flight f in flights)
            {
                Console.WriteLine(f.ToString());
            }

            Console.WriteLine(fl.GetFlight(2).ToString());
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
