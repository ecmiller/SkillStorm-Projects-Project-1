using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirlineService.Controllers
{
    public class PassengersController : Controller
    {
        // GET: Passengers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Passengers/Details/5
        public ActionResult Details(int id)
        {
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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Passengers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Passengers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Passengers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Passengers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}