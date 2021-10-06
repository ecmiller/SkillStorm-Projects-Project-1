using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.Data;
using AirlineService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class FlightsController : Controller
    {
        private readonly IFlightDAO flightDAO;

        
        public FlightsController(IFlightDAO FlightDao)
        {
            this.flightDAO = FlightDao;
        }

        public IActionResult Index()
        {
            IEnumerable<Flight> mFlights = flightDAO.GetFlights();
            List<FlightViewModel> model = new List<FlightViewModel>();

            foreach (var flight in mFlights)
            {
                FlightViewModel temp = new FlightViewModel
                {
                    FlightID = flight.FlightID,
                    Airline = flight.Airline,
                    DepartureLocation = flight.DepartureLocation,
                    DepartureTime = flight.DepartureTime,
                    ArrivalLocation = flight.ArrivalLocation,
                    ArrivalTime = flight.ArrivalTime,
                    SeatsRemaining = flight.SeatsRemaining

                };

                model.Add(temp);
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            Flight model = flightDAO.GetFlight(id);

            return View(model);
        }


        // Edit Action
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Flight model = flightDAO.GetFlight(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind] FlightViewModel flight)
        {
            Flight newFlight = flightDAO.GetFlight(flight.FlightID);

            Console.WriteLine("Flight sent into the edit call " + flight.ToString());
            if (ModelState.IsValid)
            {
                newFlight.Airline = flight.Airline;
                newFlight.DepartureLocation = flight.DepartureLocation;
                newFlight.DepartureTime = flight.DepartureTime;
                newFlight.ArrivalLocation = flight.ArrivalLocation;
                newFlight.ArrivalTime = flight.ArrivalTime;
                newFlight.SeatsRemaining = flight.SeatsRemaining;
                newFlight.MaxCapacity = flight.MaxCapacity;

                Console.WriteLine("Old flight data: " + flightDAO.GetFlight(flight.FlightID).ToString());
                flightDAO.UpdateFlight(newFlight);
                Console.WriteLine("New flight data" + flightDAO.GetFlight(flight.FlightID).ToString());

                return RedirectToAction("Index");
            } else
            {
                Console.WriteLine("Update ModelState was invalid");
            }

            return View(flight);
        }


        // Create Action
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] FlightViewModel flight)
        {
            if (ModelState.IsValid)
            {
                Flight newFlight = new Flight();
                newFlight.Airline = flight.Airline;
                newFlight.DepartureLocation = flight.DepartureLocation;
                newFlight.DepartureTime = flight.DepartureTime;
                newFlight.ArrivalLocation = flight.ArrivalLocation;
                newFlight.ArrivalTime = flight.ArrivalTime;
                newFlight.SeatsRemaining = flight.SeatsRemaining;
                newFlight.MaxCapacity = flight.MaxCapacity;

                Console.WriteLine("Adding flight " + newFlight.ToString());
                flightDAO.AddFlight(newFlight);

                return RedirectToAction("Index");
            }

            return View(flight);
        }
    }
}