using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.Data;
using AirlineService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightDAO flightDAO;

        
        public FlightController(IFlightDAO FlightDao)
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

                flightDAO.AddFlight(newFlight);

                return RedirectToAction("Index");
            }

            return View(flight);
        }
    }
}