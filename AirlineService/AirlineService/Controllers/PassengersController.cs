using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.Data;
using AirlineService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class PassengersController : Controller
    {
        private readonly IPassengerDAO passengerDAO;

        public PassengersController(IPassengerDAO passengerDAO)
        {
            this.passengerDAO = passengerDAO;
        }

        // GET: Passengers
        public IActionResult Index()
        {
            IEnumerable<Passenger> mPassengers = passengerDAO.GetPassengers();
            List<PassengerViewModel> model = new List<PassengerViewModel>();

            foreach (var pass in mPassengers)
            {
                PassengerViewModel temp = new PassengerViewModel
                {
                    PassengerID = pass.PassengerID,
                    Name = pass.Name,
                    Age = pass.Age,
                    Email = pass.Email,
                    Bookings = pass.Bookings
                };

                // --- TESTING ---
                // Console.WriteLine("Made a PassengerViewModel -- " + temp.ToString());
                model.Add(temp);
            }

            return View(model);
        }

        // GET: Passengers/Details/5
        public IActionResult Details(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // GET: Passengers/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if(ModelState.IsValid)
            {
                Passenger newPassenger = new Passenger();
                newPassenger.Name = collection["Name"];
                newPassenger.Age = int.Parse(collection["Age"]);
                newPassenger.Email = collection["Email"];
                newPassenger.Bookings = new List<string>();

                passengerDAO.AddPassenger(newPassenger);

                return RedirectToAction("Index");
            } else
            {
                Console.WriteLine("The Passenger you are trying to add is not valid");
            }

            return View();
        }

        // GET: Passengers/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            Console.WriteLine("Called the edit command for id: " + id);
            Passenger newPassenger = passengerDAO.GetPassenger(id);
            newPassenger.Name = collection["Name"];
            newPassenger.Age = int.Parse(collection["Age"]);
            newPassenger.Email = collection["Email"];

            if(ModelState.IsValid)
            {
                passengerDAO.UpdatePassenger(newPassenger);

                return RedirectToAction("Index");
            } else
            {
                Console.WriteLine("[Update Action] ModelState was invalid");
            }

            return View();
        }

        // GET: Passengers/Delete/5
        public IActionResult Delete(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // POST: Flights/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            passengerDAO.RemovePassenger(id);

            return RedirectToAction("Index");
        }
    }
}