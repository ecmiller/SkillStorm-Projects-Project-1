using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineService.Data;
using AirlineService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IBookingDAO bookingsDAO;

        public BookingsController(IBookingDAO BookingDao)
        {
            this.bookingsDAO = BookingDao;
        }

        // GET: Bookings
        public IActionResult Index()
        {
            IEnumerable<Booking> mBookings = bookingsDAO.GetAllBookings();
            List<BookingViewModel> model = new List<BookingViewModel>();

            foreach (var booking in mBookings)
            {
                BookingViewModel temp = new BookingViewModel
                {
                    BookingID = booking.BookingID,
                    PassengerID = booking.PassengerID,
                    FlightID = booking.FlightID,
                    ConfirmationNumber = booking.ConfirmationNumber
                };

                // --- TESTING ---
                // Console.WriteLine("Made a BookingViewModel -- " + temp.ToString());
                model.Add(temp);
            }

            return View(model);
        }

        // GET: Bookings/Details/5
        public IActionResult Details(int id)
        {
            Booking model = bookingsDAO.GetBooking(id);
            return View(model);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int pid = int.Parse(collection["PassengerID"]);
                int fid = int.Parse(collection["FlightID"]);

                bookingsDAO.BookFlight(pid, fid);
                return RedirectToAction("Index");
            } else
            {
                Console.WriteLine("The Booking you are trying to add is not valid");
            }

            return View();
        }


        // GET: Bookings/Delete/5
        public IActionResult Delete(int id)
        {
            Booking model = bookingsDAO.GetBooking(id);
            return View(model);
        }

        // POST: Bookings/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            Booking booking = bookingsDAO.GetBooking(id);
            bookingsDAO.RemoveBooking(booking);

            return RedirectToAction("Index");
        }
    }
}