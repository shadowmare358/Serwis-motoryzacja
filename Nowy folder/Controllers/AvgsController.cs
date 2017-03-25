using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracaInz.Models;

namespace PracaInz.Controllers
{
    public class AvgsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Avgs

        public ActionResult Index(string search)
        {
            var averages = from s in db.Averages
                       select s;
            //var averages = db.Averages.OrderByDescending(a => a.Srednia);
            if (!String.IsNullOrEmpty(search))
            {
                averages = averages.Where(s => s.Car.Model.Contains(search));
            }
            averages = averages.OrderByDescending(s => s.Srednia);
            return View(averages.ToList());
        }

        // GET: Avgs/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Average average = db.Averages.Find(id);
            if (average == null)
            {
                return HttpNotFound();
            }
            return View(average);
        }

        // GET: Avgs/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model");
            return View();
        }

        // POST: Avgs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CarId,Srednia")] Average average)
        {
            if (ModelState.IsValid)
            {
                db.Averages.Add(average);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", average.CarId);
            return View(average);
        }

        // GET: Avgs/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Average average = db.Averages.Find(id);
            if (average == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", average.CarId);
            return View(average);
        }

        // POST: Avgs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit([Bind(Include = "Id,CarId,Srednia")] Average average)
        {
            if (ModelState.IsValid)
            {
                db.Entry(average).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.Cars, "CarId", "Model", average.CarId);
            return View(average);
        }

        // GET: Avgs/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Average average = db.Averages.Find(id);
            if (average == null)
            {
                return HttpNotFound();
            }
            return View(average);
        }

        // POST: Avgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Average average = db.Averages.Find(id);
            db.Averages.Remove(average);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
