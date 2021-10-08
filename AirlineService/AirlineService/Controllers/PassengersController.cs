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
        public ActionResult Index()
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
                // Console.WriteLine("Made a flightviewmodel -- " + temp.ToString());
                model.Add(temp);
            }

            return View(model);
        }

        // GET: Passengers/Details/5
        public ActionResult Details(int id)
        {
            Passenger passenger = passengerDAO.GetPassenger(id);
            return View();
        }

        // GET: Passengers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Passengers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] PassengerViewModel passenger)
        {
            if(ModelState.IsValid)
            {
                Passenger newPassenger = new Passenger();
                newPassenger.Name = passenger.Name;
                newPassenger.Age = passenger.Age;
                newPassenger.Email = passenger.Email;
                newPassenger.Bookings = passenger.Bookings;

                Console.WriteLine("Adding passenger " + passenger.ToString());
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Passengers/Edit/5
        public ActionResult Edit(int id)
        {
            Passenger model = passengerDAO.GetPassenger(id);
            return View(model);
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Passenger newPassenger = new Passenger();
            newPassenger.PassengerID = id;
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
        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index");
        }

        // POST: Passengers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                passengerDAO.RemovePassenger(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}